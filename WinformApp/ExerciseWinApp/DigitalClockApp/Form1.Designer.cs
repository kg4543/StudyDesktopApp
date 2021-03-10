
namespace DigitalClockApp
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.LvlClock = new System.Windows.Forms.Label();
            this.Mytimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // LvlClock
            // 
            this.LvlClock.AutoSize = true;
            this.LvlClock.Font = new System.Drawing.Font("맑은 고딕", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.LvlClock.Location = new System.Drawing.Point(12, 9);
            this.LvlClock.Name = "LvlClock";
            this.LvlClock.Size = new System.Drawing.Size(471, 65);
            this.LvlClock.TabIndex = 0;
            this.LvlClock.Text = "2021-03-10 16:17:20";
            // 
            // Mytimer
            // 
            this.Mytimer.Enabled = true;
            this.Mytimer.Interval = 1000;
            this.Mytimer.Tick += new System.EventHandler(this.Mytimer_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(486, 86);
            this.Controls.Add(this.LvlClock);
            this.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LvlClock;
        private System.Windows.Forms.Timer Mytimer;
    }
}

