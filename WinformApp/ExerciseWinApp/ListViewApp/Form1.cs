using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListViewApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListViewItem nintendo_switch = new ListViewItem();
            ListViewItem nintendo_Ds = new ListViewItem();
            ListViewItem nintendo_wii = new ListViewItem();
            ListViewItem playstation = new ListViewItem();
            ListViewItem nintendo_Xbox = new ListViewItem();

            //LsvProduct.Items.AddRange(new string[] { "Nintendo Switch", "600,000", "10", "6,000,000" });
        }

        private void RdbDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbDetails.Checked) LsvProduct.View = View.Details;
        }

        private void RdbList_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbList.Checked) LsvProduct.View = View.List;
        }

        private void RdbSmall_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbSmall.Checked) LsvProduct.View = View.SmallIcon;
        }

        private void RdbLarge_CheckedChanged(object sender, EventArgs e)
        {
            if (RdbLarge.Checked) LsvProduct.View = View.LargeIcon;
        }

        private void LsvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            TxtSelected.Text = string.Empty;

            var selected = LsvProduct.SelectedItems;

            foreach (ListViewItem item in selected)
            {
                for (int i = 0; i < 4; i++)
                {
                    TxtSelected.Text += item.SubItems[i].Text + " ";
                }
            }
        }
    }
}
