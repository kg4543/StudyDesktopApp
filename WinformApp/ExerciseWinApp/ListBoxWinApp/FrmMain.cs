using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListBoxWinApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // 살기좋은 도시 초기화
            LsbGoodCity.Items.Add("오스트리아, 빈");
            LsbGoodCity.Items.Add("호주, 멜버른");
            LsbGoodCity.Items.Add("일본, 오사카");
            LsbGoodCity.Items.Add("캐나다, 캘거리");
            LsbGoodCity.Items.Add("호주, 시드니");
            LsbGoodCity.Items.Add("캐나다, 밴쿠버");
            LsbGoodCity.Items.Add("캐나다, 토론토");
            LsbGoodCity.Items.Add("덴마크, 코페하겐");
            LsbGoodCity.Items.Add("호주, 애들레이드");
            LsbGoodCity.Items.Add("일본, 도쿄");

            List<string> lstCountry = new List<string>()
            {
                "미국","러시아","중국","영국","독일","프랑스","일본","이스라엘","사우디아라비아"
            };
            LsbHappyCountry.DataSource = lstCountry;
        }

        //sender는 원래 list형태이나 object로 박싱되어 list기능을 쓰기위해서는 다시 언박싱이 필요
        private void LsbGDPCountry_SelectedIndexChanged(object sender, EventArgs e) 
        {
            // 선택된 값 확인
            //MessageBox.Show(sender.ToString());
            var selItem = sender as ListBox; // ( ) 값 형식 변환 / as 참조 형식 변환
            // MessageBox.Show($"{selItem.SelectedIndex} : {selItem.SelectedItem}");
            TxtGDPIndex.Text = selItem.SelectedIndex.ToString();
            //TxtGDPItem.Text = selItem.SelectedItem.ToString();
            TxtGDPItem.Text = selItem.SelectedItem == null ? string.Empty : selItem.SelectedItem.ToString();
        }

        private void LsbGoodCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selItem = sender as ListBox; // ( ) 값 형식 변환 / as 참조 형식 변환
            // MessageBox.Show($"{selItem.SelectedIndex} : {selItem.SelectedItem}");
            TxtGoodIndex.Text = selItem.SelectedIndex.ToString();
            //TxtGoodItem.Text = selItem.SelectedItem.ToString();
            TxtGoodItem.Text = selItem.SelectedItem == null ? string.Empty : selItem.SelectedItem.ToString();
        }

        private void LsbHappyCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selItem = sender as ListBox; // ( ) 값 형식 변환 / as 참조 형식 변환
            // MessageBox.Show($"{selItem.SelectedIndex} : {selItem.SelectedItem}");
            TxtHappyIndex.Text = selItem.SelectedIndex.ToString();
            // TxtHappyItem.Text = selItem.SelectedItem.ToString();
            TxtHappyItem.Text = selItem.SelectedItem == null ? string.Empty : selItem.SelectedItem.ToString();
        }

        private void BtnFinish_Click(object sender, EventArgs e)
        {
            LsbGDPCountry.SelectedIndex =
            LsbGoodCity.SelectedIndex =
            LsbHappyCountry.SelectedIndex = -1;

        }
    }
}
