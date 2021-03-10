using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DateCalcApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void DtpBirthday_ValueChanged(object sender, EventArgs e)
        {
            DateTime today = DateTime.Today;
            DateTime birthday = DtpBirthday.Value;

            TxtResult.Text = $"{today.Subtract(birthday).TotalDays: #,##}";
            TxtYear.Text = (today.Subtract(birthday).TotalDays / 365).ToString("0");
        }
    }
}
