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
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Btnregen_Click(object sender, EventArgs e)
        {
            GenNewChart();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = "중간고사 성적";
            ChtScore.Titles.Add("중간고사 성적");
            GenNewChart();
        }

        private void GenNewChart()
        {
            Random rand = new Random();
            ChtScore.Series["Score"].Points.Clear();
            for (int i = 0; i < 10; i++)
            {
                ChtScore.Series["Score"].Points.Add(rand.Next(10,100));
            }
            ChtScore.Series[0].LegendText = "수학"; // score 말고 0도 가능 배열
            ChtScore.Series[0].ChartType = SeriesChartType.Spline;
        }
    }
}
