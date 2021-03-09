using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorChangeApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void Tracbar_Scroll(object sender, EventArgs e)
        {
            TxtRed.Text = TrbRed.Value.ToString();
            TxtGreen.Text = TrbGreen.Value.ToString();
            TxtBlue.Text = TrbBlue.Value.ToString();

            PnlResult.BackColor = Color.FromArgb(TrbRed.Value, TrbGreen.Value, TrbBlue.Value);

        }

        private void BtnOpen_Click(object sender, EventArgs e)
        {
            /*OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();*/
            openFileDialog1.ShowDialog();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
        }

        private void BtnColor_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
        }
    }
}
