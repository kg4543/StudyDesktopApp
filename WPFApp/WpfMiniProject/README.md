# MovieFinder using OpenAPI

<kbd>[![Movie](/Capture/MovieApi/Movie.gif "Movie")](https://github.com/kg4543/StudyDesktopApp/blob/main/WPFApp/WpfMiniProject/NaverMovieFinderApp/MainWindow.xaml.cs)</kbd>

# Search

<kbd>![Main](/Capture/MovieApi/Main.PNG "Main")</kbd>

- Naver OpenAPI를 활용 **(openApiUrl, clientID, clientSecret)**
```
public static string GetOpenApiResult(string openApiUrl, string clientID, string clientSecret)
{
    var result = "";

    try
    {
        WebRequest request = WebRequest.Create(openApiUrl);
        request.Headers.Add("X-Naver-Client-ID", clientID);
        request.Headers.Add("X-Naver-Client-Secret", clientSecret);

        WebResponse response = request.GetResponse();
        Stream stream = response.GetResponseStream();
        StreamReader reader = new StreamReader(stream);

        result = reader.ReadToEnd();

        reader.Close();
        stream.Close();
        response.Close();

    }
    catch (Exception ex)
    {
        Console.WriteLine($"예외 발생 : {ex}");
    }

    return result;
}
```
- 받은 결과 값을 Json 형태로 변환하여 MovieItem형태의 List에 받아서 출력
- MovieItem은 Common에서 class를 선언하여 객체를 생성할 수 있도록 함
```
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
```
- 받은 결과값의 HTML태그는 제거
```
public static string StripHtmlTag(string text)
{
    return Regex.Replace(text, @"<(.|\n)*?>", ""); //HTML 태그 삭제하는 정규 표현식
}

public static string StripPipe(string text)
{
    if (string.IsNullOrEmpty(text))
        return "";
    return text.Substring(0, text.LastIndexOf("|")).Replace("|", ", ");

    /*string result = text.Replace("|",", ");
    return result.Substring(0, result.LastIndexOf(","));*/
}

public static string Stripamp(string text)
{
    if (string.IsNullOrEmpty(text))
        return "";
    return text.Replace("&amp;", "");
}
```

# Favorit

<kbd>![즐겨찾기](/Capture/MovieApi/즐겨찾기.PNG "즐겨찾기")</kbd>

- Entity Framework를 활용하여 DB를 불러와 데이터 로드 및 수정
- 'isFavorite'을 통해 데이터 중복 방지
- 선택한 영화들을 List를 선언해 추가
```
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
```

# Youtube

<kbd>![예고편보기](/Capture/MovieApi/예고편보기.PNG "예고편보기")</kbd>

- YoutubeAPI를 활용하여 Youtube 검색 및 결과 불러오기
- YoutubeItem class를 선언하여 객체 생성
```
private async Task LoadDataCollection()
{
    var youtubeService = new YouTubeService(
        new BaseClientService.Initializer()
        {
            ApiKey = "AIzaSyDwUh5aEYp7tTNKfTLUWmrh13Qcp2p1-IQ",
            ApplicationName = this.GetType().ToString()
        });

    var request = youtubeService.Search.List("snippet");
    request.Q = LblMovieName.Content.ToString(); // {영화이름} 예고편
    request.MaxResults = 10; // size가 클 경우 멈춰버림

    var response = await request.ExecuteAsync(); //검색시작(youtube OpenApi 호출)

    foreach (var item in response.Items)
    {
        if (item.Id.Kind.Equals("youtube#video"))
        {
            YoutubeItem youtube = new YoutubeItem()
            {
                Title = item.Snippet.Title,
                Author = item.Snippet.ChannelTitle,
                URL = $"https://www.youtube.com/watch?v={item.Id.VideoId}"
            };
            //썸네일 이미지
            youtube.Thumbnail = new BitmapImage(new Uri(item.Snippet.Thumbnails.Default__.Url, UriKind.RelativeOrAbsolute));
                youtubes.Add(youtube);
        }
    }
}
```
- 더블 클릭 시 영상 불러오기
```
if (LsvYoutubeSearch.SelectedItem is YoutubeItem)
{
    var video = LsvYoutubeSearch.SelectedItem as YoutubeItem;
    BrwYoutubeWatch.Source = new Uri(video.URL, UriKind.RelativeOrAbsolute);
}
```
