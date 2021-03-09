using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginWinApp
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (TxtId.Text.ToLower() == "admin" && TxtPassward.Text == "123456")
                TxtResult.Text = "로그인 성공";
            else
                TxtResult.Text = "로그인 실패";
        }
    }
}
