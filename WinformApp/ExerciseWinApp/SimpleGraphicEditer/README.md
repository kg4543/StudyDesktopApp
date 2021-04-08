# Paint

<kbd>![Paint](/Capture/WinForm/그림판.PNG "그림판")</kbd>

### 1. 그리기 기본 설정

* (using System.Drawing;) 사용
* DrawMode 객체를 설정
* DrawMode의 객체를 선택하여 그리기 모드 설정 
* 그릴 때 필요한 식별자 선언
* 
```
enum DrawMode
    {
        LINE,
        RECTANGLE,
        CIRCLE,
        CURVED_LINE
    }

private DrawMode mode;
private Graphics g;
private Pen pen;
private Pen erase;
private Point startP, endP, currP, prevP; 
        
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
```

### 2. 그리기

* 마우스의 움직임과 선택한 그리기 모드에 따라 그림 그리기
```
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
```

### 3. 색상 설정

* ColorDialog를 활용하여 색상 선택
```
private void TlmColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.ShowDialog();
            this.pen.Color = dialog.Color;
        }
```
