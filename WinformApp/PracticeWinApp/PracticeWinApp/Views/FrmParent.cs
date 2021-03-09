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
    public partial class FrmParent : Form
    {
        public FrmParent()
        {
            InitializeComponent();
        }

        private void FrmParent_Load(object sender, EventArgs e)
        {
            this.ClientSize = new Size(800, 600);

            FrmChild frm = new FrmChild(); // 같은 공간(View 폴더)에 위치하고 있어서 'using PracticeWinApp.view' 필요없슴
            this.AddOwnedForm(frm); //무조건 부모창 위에 자식 창이 오픈됨, 컨트롤은 상관없슴

            frm.Show();
        }
    }
}
