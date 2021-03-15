using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinChartApp
{
    public partial class FrmSub : Form
    {
        public FrmSub()
        {
            InitializeComponent();
        }

        private void FrmSub_Load(object sender, EventArgs e)
        {
            this.Text = "중간고사 성적2";
            ChtScore.Titles.Add("중간고사 성적");
            ChtScore.Series.Add("Series2"); //새로운 데이터시리즈 추가 (영어 점수)
            ChtScore.Series["Series1"].LegendText = "수학";
            ChtScore.Series["Series2"].LegendText = "영어";

            ChtScore.ChartAreas.Add("ChartArea2");
            ChtScore.Series["Series2"].ChartArea = "ChartArea2";

            Random rand = new Random();
            for (int i = 0; i < 10; i++)
            {
                ChtScore.Series[0].Points.Add(rand.Next(10, 100)); //수학
                ChtScore.Series[1].Points.Add(rand.Next(10, 100)); //영어
            }

            /*ChtScore.Series[0].ChartType = SeriesChartType.Spline;
            ChtScore.Series[1].ChartType = SeriesChartType.Spline;*/


            BtnSplit.Enabled = false;
        }

        private void BtnMerge_Click(object sender, EventArgs e)
        {
            ChtScore.ChartAreas.RemoveAt(ChtScore.ChartAreas.IndexOf("ChartArea2"));
            ChtScore.Series["Series2"].ChartArea = "ChartArea1";

            BtnMerge.Enabled = false;
            BtnSplit.Enabled = true;
        }

        private void BtnSplit_Click(object sender, EventArgs e)
        {
            ChtScore.ChartAreas.Add("ChartArea2");
            ChtScore.Series["Series2"].ChartArea = "ChartArea2";

            BtnMerge.Enabled = true;
            BtnSplit.Enabled = false;
        }
    }
}
