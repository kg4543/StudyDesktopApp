
namespace MoviePlayerApp
{
    partial class FrmPlayer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPlayer));
            this.MyPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.BtnSelec = new System.Windows.Forms.Button();
            this.DlgSelectFile = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.MyPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // MyPlayer
            // 
            this.MyPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MyPlayer.Enabled = true;
            this.MyPlayer.Location = new System.Drawing.Point(12, 12);
            this.MyPlayer.Name = "MyPlayer";
            this.MyPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("MyPlayer.OcxState")));
            this.MyPlayer.Size = new System.Drawing.Size(528, 300);
            this.MyPlayer.TabIndex = 0;
            // 
            // BtnSelec
            // 
            this.BtnSelec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelec.Location = new System.Drawing.Point(463, 319);
            this.BtnSelec.Name = "BtnSelec";
            this.BtnSelec.Size = new System.Drawing.Size(75, 23);
            this.BtnSelec.TabIndex = 1;
            this.BtnSelec.Text = "파일선택";
            this.BtnSelec.UseVisualStyleBackColor = true;
            this.BtnSelec.Click += new System.EventHandler(this.BtnSelec_Click);
            // 
            // DlgSelectFile
            // 
            this.DlgSelectFile.FileName = "openFileDialog1";
            // 
            // FrmPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(552, 355);
            this.Controls.Add(this.BtnSelec);
            this.Controls.Add(this.MyPlayer);
            this.Name = "FrmPlayer";
            this.Text = "동영상 플레이어";
            this.Load += new System.EventHandler(this.FrmPlayer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MyPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer MyPlayer;
        private System.Windows.Forms.Button BtnSelec;
        private System.Windows.Forms.OpenFileDialog DlgSelectFile;
    }
}

