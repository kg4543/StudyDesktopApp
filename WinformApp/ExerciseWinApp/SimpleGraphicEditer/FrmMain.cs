using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleGraphicEditer
{
    enum DrawMode
    {
        LINE,
        RECTANGLE,
        CIRCLE,
        CURVED_LINE
    }

    public partial class FrmMain : Form
    {
        private DrawMode mode;
        private Graphics g;
        private Pen pen;
        private Pen erase;
        private Point startP, endP, currP, prevP; 

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            StsCurrent.Text = "";

            g = CreateGraphics();
            pen = new Pen(Color.Black, 1);
            StsCurrent.Text = "Line Mode";
            this.mode = DrawMode.LINE;
            this.BackColor = Color.White;
            this.erase = new Pen(Color.White);
        }

        private void TlmLine_Click(object sender, EventArgs e)
        {
            this.mode = DrawMode.LINE;
            StsCurrent.Text = "Line Mode";
        }

        private void TlmRectangle_Click(object sender, EventArgs e)
        {
            this.mode = DrawMode.RECTANGLE;
            StsCurrent.Text = "Rectangle Mode";
        }

        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            this.startP = new Point(e.X, e.Y);
            this.currP = this.prevP = this.startP;
            this.endP = new Point(e.X, e.Y);
            switch (this.mode)
            {
                case DrawMode.LINE:
                    g.DrawLine(this.pen, this.startP, this.endP);
                    break;
                case DrawMode.RECTANGLE:
                    g.DrawRectangle(this.pen,
                        new Rectangle(startP, new Size(endP.X - startP.X, endP.Y - startP.Y)));
                    break;
                case DrawMode.CIRCLE:
                    g.DrawEllipse(this.pen,
                        new Rectangle(startP, new Size(endP.X - startP.X, endP.Y - startP.Y)));
                    break;
                case DrawMode.CURVED_LINE:
                    break;
                default:
                    break;
            }
        }

        //마우스를 옮길때 마다 발생
        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            this.prevP = this.currP;
            this.currP = new Point(e.X,e.Y);
            switch (this.mode)
            {
                case DrawMode.LINE:
                    g.DrawLine(this.erase, this.startP, this.prevP);
                    g.DrawLine(this.pen, this.startP, this.currP);
                    break;
                case DrawMode.RECTANGLE:
                    g.DrawRectangle(this.erase, 
                        new Rectangle(startP, new Size(prevP.X - startP.X, prevP.Y - startP.Y)));
                    g.DrawRectangle(this.pen,
                        new Rectangle(startP, new Size(currP.X - startP.X, currP.Y - startP.Y)));
                    break;
                case DrawMode.CIRCLE:
                    g.DrawEllipse(this.erase,
                       new Rectangle(startP, new Size(prevP.X - startP.X, prevP.Y - startP.Y)));
                    g.DrawEllipse(this.pen,
                        new Rectangle(startP, new Size(currP.X - startP.X, currP.Y - startP.Y)));
                    break;
                case DrawMode.CURVED_LINE:
                    break;
                default:
                    break;
            }
        }

        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            this.endP = new Point(e.X,e.Y);
            switch (this.mode)
            {
                case DrawMode.LINE:
                    g.DrawLine(this.pen, this.startP, this.endP);
                    break;
                case DrawMode.RECTANGLE:
                    g.DrawRectangle(this.pen,
                        new Rectangle(startP, new Size(endP.X - startP.X, endP.Y - startP.Y)));
                    break;
                case DrawMode.CIRCLE:
                    g.DrawEllipse(this.pen,
                        new Rectangle(startP, new Size(endP.X - startP.X, endP.Y - startP.Y)));
                    break;
                case DrawMode.CURVED_LINE:
                    break;
                default:
                    break;
            }
        }

        private void TlmCircle_Click(object sender, EventArgs e)
        {
            this.mode = DrawMode.CIRCLE;
            StsCurrent.Text = "Circle Mode";
        }

        private void TlmCurvedLine_Click(object sender, EventArgs e)
        {
            this.mode = DrawMode.CURVED_LINE;
            StsCurrent.Text = "CurvedKine Mode";
        }

        private void TlmColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();
            this.pen.Color = dialog.Color;
        }
    }
}
