using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeWinApp.Views
{
    public partial class FrmChild : Form
    {
        public FrmChild()
        {
            InitializeComponent();
        }

        private void FrmChild_Load(object sender, EventArgs e)
        {
            /*this.ClientSize = new Size(300,100);
            this.Text = "Child";*/

            this.CenterToParent(); // 부모창 정중앙에서 오픈
            
        }

        private void BtnQuestion_Click(object sender, EventArgs e)
        {
             if(MessageBox.Show("정답입니까?","질문",MessageBoxButtons.YesNo,MessageBoxIcon.Question)
                == DialogResult.Yes)
             {
                MessageBox.Show("정답입니다.");
             }
            else
            {
                MessageBox.Show("틀렸습니다.");
            }

        }
    }
}
