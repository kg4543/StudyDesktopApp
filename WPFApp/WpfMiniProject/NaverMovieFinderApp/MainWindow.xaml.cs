using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using NaverMovieFinderApp.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace NaverMovieFinderApp
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            TxtMovieName.Focus();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"즐겨찾기 여부 : {Commons.IsFavorite}");
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            StsResult.Content = "";
            ImgPoster.Source = new BitmapImage(new Uri("No_Picture.jpg", UriKind.RelativeOrAbsolute));

            if (string.IsNullOrEmpty(TxtMovieName.Text))
            {
                StsResult.Content = "검색할 영화명을 입력 후, 검색버튼을 눌러주세요!!!";
                Commons.ShowMessageAsync("검색", "검색할 영화명을 입력 후, 검색버튼을 눌러주세요!");
                return;
            }

            //Commons.ShowMessageAsync("결과", $"{TxtMovieName.Text}");
            try
            {
                ProcSearchNaverApi(TxtMovieName.Text);
                Commons.ShowMessageAsync("검색","영화 검색 완료");
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외",$"예외발생 : {ex}");
                Commons.LOGGER.Error("예외", $"예외발생 : {ex}");
            }

            Commons.IsFavorite = false; //즐겨찾기 아님
        }

        private void ProcSearchNaverApi(string movieName)
        {
            string clientID = "Jj1Shad6ZWLtrFfb1bBd";
            string clientSecret = "b06WyD2SYf";
            string openApiUrl = $"https://openapi.naver.com/v1/search/movie?start=1&display=30&query={movieName}";

            string resJson = Commons.GetOpenApiResult(openApiUrl, clientID, clientSecret);
            var parsedJson = JObject.Parse(resJson);

            int total = Convert.ToInt32(parsedJson["total"]);
            int display = Convert.ToInt32(parsedJson["display"]);

            StsResult.Content = $"{total} 중 {display} 호출 성공";

            var items = parsedJson["items"];
            var json_array = (JArray)items;

            List<MovieItem> movieItems = new List<MovieItem>();

            foreach (var item in json_array)
            {
                MovieItem movie = new MovieItem(
                    Commons.Stripamp(Commons.StripHtmlTag(item["title"].ToString())),
                    item["link"].ToString(),
                    item["image"].ToString(),
                    Commons.Stripamp(item["subtitle"].ToString()),
                    item["pubDate"].ToString(),
                    Commons.StripPipe(item["director"].ToString()),
                    Commons.StripPipe(item["actor"].ToString()),
                    item["userRating"].ToString()
                    );
                movieItems.Add(movie);
            }

            this.DataContext = movieItems;
        }

        private void TxtMovieName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                BtnSearch_Click(sender, e);
            }
        }

        private void DataGrid_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        {
            /*if (GrdData.SelectedItem == null)
            {
                Commons.ShowMessageAsync("오류", "영화를 선택하세요!!");
                return;
            }*/

            if (GrdData.SelectedItem is MovieItem)
            {
                var movie = GrdData.SelectedItem as MovieItem;

                if (string.IsNullOrEmpty(movie.Image))
                {
                    ImgPoster.Source = new BitmapImage(new Uri("No_Picture.jpg", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ImgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
                    //ImgPoster.Stretch = System.Windows.Media.Stretch.Uniform;
                }
            }

            if (GrdData.SelectedItem is NaverFavoriteMovies)
            {
                var movie = GrdData.SelectedItem as NaverFavoriteMovies;

                if (string.IsNullOrEmpty(movie.Image))
                {
                    ImgPoster.Source = new BitmapImage(new Uri("No_Picture.jpg", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    ImgPoster.Source = new BitmapImage(new Uri(movie.Image, UriKind.RelativeOrAbsolute));
                    ImgPoster.Stretch = System.Windows.Media.Stretch.Uniform;
                }
            }
        }

        private void BtnAddWatchList_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("Info", "즐겨찾기에 추가할 영화를 선택하세요(복수선택 가능)");
                return;
            }

            if (Commons.IsFavorite)
            {
                // 이미 즐겨찾기한 내용을 다시 즐겨찾기 하는 것을 방지
                Commons.ShowMessageAsync("즐겨찾기","즐겨찾기 조회내용을 다시 즐겨찾기 할 수 없습니다.");
                return;
            }

            List<NaverFavoriteMovies> list = new List<NaverFavoriteMovies>();

            foreach (MovieItem item in GrdData.SelectedItems)
            {
                NaverFavoriteMovies temp = new NaverFavoriteMovies()
                {
                    Title = item.Title,
                    Link = item.Link,
                    Image = item.Image,
                    SubTitle = item.SubTitle,
                    PubDate = item.PubDate,
                    Director = item.Director,
                    Actor = item.Actor,
                    UserRating = item.UserRating,
                    RegDate = DateTime.Now
                };

                list.Add(temp);
            }

            try
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    // ctx.NaverFavoriteMovies.AddRange(list) 와 동일
                    ctx.Set<NaverFavoriteMovies>().AddRange(list);
                    ctx.SaveChanges();
                }

                Commons.ShowMessageAsync("저장", "즐겨찾기에 추가했습니다.");
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"에외발생 : {ex}");
                Commons.LOGGER.Error("예외", $"예외발생 : {ex}");
            }
        }

        private void BtnViewWatchList_Click(object sender, RoutedEventArgs e)
        {
            
            this.DataContext = null;
            TxtMovieName.Text = "";

            //List<MovieItem> listData = new List<MovieItem>();
            List<NaverFavoriteMovies> list = new List<NaverFavoriteMovies>();
            try
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    list = ctx.NaverFavoriteMovies.ToList();
                }
                this.DataContext = list;
                StsResult.Content = $"즐겨찾기 {list.Count}개 조회";

                if (Commons.IsDelete)
                    Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 삭제 완료");
                    
                else
                    Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 조회 완료");
                
                Commons.IsFavorite = true;
                Commons.IsDelete = false;
                ImgPoster.Source = new BitmapImage(new Uri("No_Picture.jpg", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                Commons.ShowMessageAsync("예외", $"에외발생 : {ex}");
                Commons.LOGGER.Error("예외", $"예외발생 : {ex}");
                Commons.IsFavorite = false;
            }

            /*foreach (var item in list)
            {
                listData.Add(new MovieItem(
                    item.Title,
                    item.Link,
                    item.Image,
                    item.SubTitle,
                    item.PubDate,
                    item.Director,
                    item.Actor,
                    item.UserRating
                    ));
            }*/
        }

        private void BtnDeleteWatchList_Click(object sender, RoutedEventArgs e)
        {
            if (Commons.IsFavorite == false)
            {
                Commons.ShowMessageAsync("즐겨찾기", "즐겨찾기 내용이 아니면 삭제할 수 없습니다.");
                return;
            }

            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("즐겨찾기", "삭제할 즐겨찾기 영화를 선택하세요.");
                return;
            }

            foreach (NaverFavoriteMovies item in GrdData.SelectedItems)
            {
                using (var ctx = new OpenApiLabEntities())
                {
                    var itemDelete = ctx.NaverFavoriteMovies.Find(item.Idx); // 삭제할 영화 객체 검색 후 생성
                    ctx.Entry(itemDelete).State = EntityState.Deleted; // 검색객체 상테를 삭제로 변경
                    ctx.SaveChanges(); //commit
                }
            }
            Commons.IsDelete = true;
            BtnViewWatchList_Click(sender, e);
        }

        private void NaverMovieLink_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("Naver영화","영화를 선택하세요.");
                return;
            }

            if (GrdData.SelectedItems.Count > 1)
            {
                Commons.ShowMessageAsync("Naver영화", "영화를 하나만 선택하세요.");
                return;
            }

            // 선택된 영화 네이버영화 URL 가져오기
            string linkUrl = "";
            if (Commons.IsFavorite)
            {
                var item = GrdData.SelectedItem as NaverFavoriteMovies;
                linkUrl = item.Link;
            }
            else
            {
                var item = GrdData.SelectedItem as MovieItem;
                linkUrl = item.Link;
            }

            Process.Start(linkUrl);
        }

        private void BtnWatchTrailer_Click(object sender, RoutedEventArgs e)
        {
            if (GrdData.SelectedItems.Count == 0)
            {
                Commons.ShowMessageAsync("Youtube영화", "영화를 선택하세요.");
                return;
            }

            if (GrdData.SelectedItems.Count > 1)
            {
                Commons.ShowMessageAsync("Youtube영화", "영화를 하나만 선택하세요.");
                return;
            }

            string movieName = "";

            if (Commons.IsFavorite)
            {
                var item = GrdData.SelectedItem as NaverFavoriteMovies;
                movieName = item.Title;
            }
            else
            {
                var item = GrdData.SelectedItem as MovieItem;
                movieName = item.Title;
            }

            var trailerWindow = new TrailerWindow(movieName);
            trailerWindow.Owner = this;
            trailerWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            trailerWindow.ShowDialog();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            /*var result = Commons.ShowMessageAsync("종료", "프로그램을 종료하시겠습니까?",MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                e.Cancel = true;
                Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = false;
                return;
            }*/
        }
    }
}
