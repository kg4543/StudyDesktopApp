using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadioWinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnResult_Click(object sender, EventArgs e)
        {
            String result = string.Empty;

            if(RdbKorea.Checked == false && RdbChaina.Checked == false 
                && RdbJapan.Checked == false && RdbOthers.Checked == false)
            {
                MessageBox.Show("국가를 선택하세요");
                return;
            }
            if(RdbMale.Checked == false && RdbFemale.Checked == false)
            {
                MessageBox.Show("성별을 선택하세요");
                return;
            }

            if (RdbKorea.Checked)
                result = "국적 : 대한민국\n";
            else if (RdbChaina.Checked)
                result = "국적 : 중국\n";
            else if (RdbJapan.Checked)
                result = "국적 : 일본\n";
            else if (RdbOthers.Checked)
                result = "국적 : 동아시아 외 국가\n";

            if (RdbMale.Checked)
                result += "성별 : 남성";
            else if (RdbFemale.Checked)
                result += "성별 : 여성";

            MessageBox.Show(result, "결과");
        }
    }
}
