using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabelTestApp
{
    public partial class FemMain : Form
    {
        public FemMain()
        {
            InitializeComponent();
        }

        private void FemMain_Load(object sender, EventArgs e)
        {
            LblAutoSize.Text = LblAutoSize.Text = string.Empty;
        }

        private void BtnPushText_Click(object sender, EventArgs e)
        {
            string sample1 = "kjds";
            string sample2 = "sadjglkfmad'pojw;flml; mpjml;sfdmmwel;mf;";

            LblAutoSize.Text = sample1;
            LblManualSize.Text = sample2;
        }
    }
}
