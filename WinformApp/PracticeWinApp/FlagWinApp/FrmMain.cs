using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlagWinApp
{
   
    public partial class FrmMain : Form
    {
        private bool isHello = false; //flag 상태를 저장하는 값

        public FrmMain()
        {
            InitializeComponent();
            isHello = true; // 아침
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            LblGreeting.Text = string.Empty;
        }

        private void BtnGreeting_Click(object sender, EventArgs e)
        {
            if (isHello == true)
            {
                LblGreeting.Text = "Good Morning~";
                isHello = false;
                BtnGreeting.Text = "저녁인사";
            }
            else if (isHello == false)
            {
                LblGreeting.Text = "Good bye~";
                isHello = false;
                BtnGreeting.Text = "아침인사";
            }
        }
    }
}
