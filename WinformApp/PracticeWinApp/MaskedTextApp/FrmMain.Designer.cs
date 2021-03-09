
namespace MaskedTextApp
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnRegister = new System.Windows.Forms.Button();
            this.TxtDate = new System.Windows.Forms.MaskedTextBox();
            this.TxtZipCode = new System.Windows.Forms.MaskedTextBox();
            this.TxtAddr = new System.Windows.Forms.MaskedTextBox();
            this.TxtMobile = new System.Windows.Forms.MaskedTextBox();
            this.TxtRegisterNum = new System.Windows.Forms.MaskedTextBox();
            this.TxtEmail = new System.Windows.Forms.MaskedTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "입사일";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "우편번호";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "주소";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "휴대폰번호";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "주민번호";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(42, 185);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "이메일";
            // 
            // BtnRegister
            // 
            this.BtnRegister.Image = global::MaskedTextApp.Properties.Resources.register;
            this.BtnRegister.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.BtnRegister.Location = new System.Drawing.Point(257, 218);
            this.BtnRegister.Name = "BtnRegister";
            this.BtnRegister.Size = new System.Drawing.Size(56, 30);
            this.BtnRegister.TabIndex = 12;
            this.BtnRegister.Text = "등록";
            this.BtnRegister.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.BtnRegister.UseVisualStyleBackColor = true;
            this.BtnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // TxtDate
            // 
            this.TxtDate.Location = new System.Drawing.Point(89, 17);
            this.TxtDate.Mask = "0000-00-00";
            this.TxtDate.Name = "TxtDate";
            this.TxtDate.Size = new System.Drawing.Size(70, 21);
            this.TxtDate.TabIndex = 13;
            this.TxtDate.ValidatingType = typeof(System.DateTime);
            // 
            // TxtZipCode
            // 
            this.TxtZipCode.Location = new System.Drawing.Point(89, 50);
            this.TxtZipCode.Mask = "99999";
            this.TxtZipCode.Name = "TxtZipCode";
            this.TxtZipCode.Size = new System.Drawing.Size(41, 21);
            this.TxtZipCode.TabIndex = 14;
            this.TxtZipCode.ValidatingType = typeof(int);
            // 
            // TxtAddr
            // 
            this.TxtAddr.Location = new System.Drawing.Point(89, 83);
            this.TxtAddr.Name = "TxtAddr";
            this.TxtAddr.Size = new System.Drawing.Size(224, 21);
            this.TxtAddr.TabIndex = 15;
            // 
            // TxtMobile
            // 
            this.TxtMobile.Location = new System.Drawing.Point(89, 116);
            this.TxtMobile.Mask = "000-9000-0000";
            this.TxtMobile.Name = "TxtMobile";
            this.TxtMobile.Size = new System.Drawing.Size(88, 21);
            this.TxtMobile.TabIndex = 16;
            // 
            // TxtRegisterNum
            // 
            this.TxtRegisterNum.Location = new System.Drawing.Point(89, 149);
            this.TxtRegisterNum.Mask = "000000-0000000";
            this.TxtRegisterNum.Name = "TxtRegisterNum";
            this.TxtRegisterNum.Size = new System.Drawing.Size(97, 21);
            this.TxtRegisterNum.TabIndex = 17;
            // 
            // TxtEmail
            // 
            this.TxtEmail.Location = new System.Drawing.Point(89, 182);
            this.TxtEmail.Name = "TxtEmail";
            this.TxtEmail.Size = new System.Drawing.Size(141, 21);
            this.TxtEmail.TabIndex = 18;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 257);
            this.Controls.Add(this.TxtEmail);
            this.Controls.Add(this.TxtRegisterNum);
            this.Controls.Add(this.TxtMobile);
            this.Controls.Add(this.TxtAddr);
            this.Controls.Add(this.TxtZipCode);
            this.Controls.Add(this.TxtDate);
            this.Controls.Add(this.BtnRegister);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "사원 정보 등록";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnRegister;
        private System.Windows.Forms.MaskedTextBox TxtDate;
        private System.Windows.Forms.MaskedTextBox TxtZipCode;
        private System.Windows.Forms.MaskedTextBox TxtAddr;
        private System.Windows.Forms.MaskedTextBox TxtMobile;
        private System.Windows.Forms.MaskedTextBox TxtRegisterNum;
        private System.Windows.Forms.MaskedTextBox TxtEmail;
    }
}

