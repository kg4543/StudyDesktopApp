
namespace FlagWinApp
{
    partial class FrmMain
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
            this.LblGreeting = new System.Windows.Forms.Label();
            this.BtnGreeting = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LblGreeting
            // 
            this.LblGreeting.AutoSize = true;
            this.LblGreeting.Location = new System.Drawing.Point(25, 59);
            this.LblGreeting.Name = "LblGreeting";
            this.LblGreeting.Size = new System.Drawing.Size(69, 12);
            this.LblGreeting.TabIndex = 0;
            this.LblGreeting.Text = "LblGreeting";
            // 
            // BtnGreeting
            // 
            this.BtnGreeting.Location = new System.Drawing.Point(65, 143);
            this.BtnGreeting.Name = "BtnGreeting";
            this.BtnGreeting.Size = new System.Drawing.Size(75, 23);
            this.BtnGreeting.TabIndex = 1;
            this.BtnGreeting.Text = "인사하기";
            this.BtnGreeting.UseVisualStyleBackColor = true;
            this.BtnGreeting.Click += new System.EventHandler(this.BtnGreeting_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(205, 178);
            this.Controls.Add(this.BtnGreeting);
            this.Controls.Add(this.LblGreeting);
            this.Name = "FrmMain";
            this.Text = "Greeting App";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblGreeting;
        private System.Windows.Forms.Button BtnGreeting;
    }
}

