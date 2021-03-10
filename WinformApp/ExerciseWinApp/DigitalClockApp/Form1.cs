using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalClockApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Mytimer.Start();
            
        }

        private void Mytimer_Tick(object sender, EventArgs e)
        {
            LvlClock.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
