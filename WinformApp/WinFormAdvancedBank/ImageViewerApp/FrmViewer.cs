using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageViewerApp
{
    public partial class FrmViewer : Form
    {
        public FrmViewer()
        {
            InitializeComponent();
        }

        private void MnuSelectImage_Click(object sender, EventArgs e)
        {
            DlgOpenImage.Filter = "jpg 파일(*jpg)|*.jpg|png 파일(*.png)|*.png|모든 파일(*.*)|*.*";
            DlgOpenImage.Title = "이미지 열기";
            if (DlgOpenImage.ShowDialog() == DialogResult.OK)
            {
                PicMain.Image = new Bitmap(DlgOpenImage.FileName);
            }
            PicMain.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void MnuExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void nomalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicMain.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void stretchImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicMain.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void autoSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicMain.SizeMode = PictureBoxSizeMode.AutoSize;
        }

        private void centerImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicMain.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void zoomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PicMain.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void FrmViewer_Load(object sender, EventArgs e)
        {
            this.Text = "이미지 뷰어";
            PicMain.BackColor = Color.LightGreen;
        }
    }
}
