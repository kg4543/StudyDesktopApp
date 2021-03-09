
namespace LoginWinApp
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtId = new System.Windows.Forms.TextBox();
            this.TxtPassward = new System.Windows.Forms.TextBox();
            this.BtnLogin = new System.Windows.Forms.Button();
            this.TxtResult = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "아이디";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "패스워드";
            // 
            // TxtId
            // 
            this.TxtId.Location = new System.Drawing.Point(90, 30);
            this.TxtId.Name = "TxtId";
            this.TxtId.Size = new System.Drawing.Size(208, 21);
            this.TxtId.TabIndex = 2;
            // 
            // TxtPassward
            // 
            this.TxtPassward.Location = new System.Drawing.Point(90, 62);
            this.TxtPassward.Name = "TxtPassward";
            this.TxtPassward.PasswordChar = '*';
            this.TxtPassward.Size = new System.Drawing.Size(208, 21);
            this.TxtPassward.TabIndex = 3;
            // 
            // BtnLogin
            // 
            this.BtnLogin.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.BtnLogin.Image = global::LoginWinApp.Properties.Resources.unlock;
            this.BtnLogin.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.BtnLogin.Location = new System.Drawing.Point(221, 89);
            this.BtnLogin.Name = "BtnLogin";
            this.BtnLogin.Size = new System.Drawing.Size(77, 40);
            this.BtnLogin.TabIndex = 4;
            this.BtnLogin.Text = "로그인";
            this.BtnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnLogin.UseVisualStyleBackColor = true;
            this.BtnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // TxtResult
            // 
            this.TxtResult.Location = new System.Drawing.Point(90, 135);
            this.TxtResult.Name = "TxtResult";
            this.TxtResult.ReadOnly = true;
            this.TxtResult.Size = new System.Drawing.Size(208, 21);
            this.TxtResult.TabIndex = 5;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 190);
            this.Controls.Add(this.TxtResult);
            this.Controls.Add(this.BtnLogin);
            this.Controls.Add(this.TxtPassward);
            this.Controls.Add(this.TxtId);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmLogin";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtId;
        private System.Windows.Forms.TextBox TxtPassward;
        private System.Windows.Forms.Button BtnLogin;
        private System.Windows.Forms.TextBox TxtResult;
    }
}

