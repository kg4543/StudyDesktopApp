
namespace MyNotePadApp
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
            this.MenuScript = new System.Windows.Forms.MenuStrip();
            this.TxtMain = new System.Windows.Forms.RichTextBox();
            this.파일ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuExite = new System.Windows.Forms.ToolStripMenuItem();
            this.편집EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.도움말HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.DlgOpenText = new System.Windows.Forms.OpenFileDialog();
            this.DlgSaveText = new System.Windows.Forms.SaveFileDialog();
            this.MenuScript.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuScript
            // 
            this.MenuScript.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.파일ToolStripMenuItem,
            this.편집EToolStripMenuItem,
            this.도움말HToolStripMenuItem});
            this.MenuScript.Location = new System.Drawing.Point(0, 0);
            this.MenuScript.Name = "MenuScript";
            this.MenuScript.Size = new System.Drawing.Size(800, 24);
            this.MenuScript.TabIndex = 0;
            this.MenuScript.Text = "menuStrip1";
            // 
            // TxtMain
            // 
            this.TxtMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtMain.Location = new System.Drawing.Point(0, 24);
            this.TxtMain.Name = "TxtMain";
            this.TxtMain.Size = new System.Drawing.Size(800, 426);
            this.TxtMain.TabIndex = 1;
            this.TxtMain.Text = "";
            this.TxtMain.TextChanged += new System.EventHandler(this.TxtMain_TextChanged);
            // 
            // 파일ToolStripMenuItem
            // 
            this.파일ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuNewFile,
            this.MnuOpen,
            this.MnuSave,
            this.MnuExite});
            this.파일ToolStripMenuItem.Name = "파일ToolStripMenuItem";
            this.파일ToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.파일ToolStripMenuItem.Text = "파일(&F)";
            // 
            // MnuNewFile
            // 
            this.MnuNewFile.Name = "MnuNewFile";
            this.MnuNewFile.Size = new System.Drawing.Size(180, 22);
            this.MnuNewFile.Text = "새로 만들기(&N)";
            this.MnuNewFile.Click += new System.EventHandler(this.MnuNewFile_Click);
            // 
            // MnuOpen
            // 
            this.MnuOpen.Name = "MnuOpen";
            this.MnuOpen.Size = new System.Drawing.Size(180, 22);
            this.MnuOpen.Text = "열기(&O)";
            this.MnuOpen.Click += new System.EventHandler(this.MnuOpen_Click);
            // 
            // MnuSave
            // 
            this.MnuSave.Name = "MnuSave";
            this.MnuSave.Size = new System.Drawing.Size(180, 22);
            this.MnuSave.Text = "저장(&S)";
            this.MnuSave.Click += new System.EventHandler(this.MnuSave_Click);
            // 
            // MnuExite
            // 
            this.MnuExite.Name = "MnuExite";
            this.MnuExite.Size = new System.Drawing.Size(180, 22);
            this.MnuExite.Text = "종료(&X)";
            this.MnuExite.Click += new System.EventHandler(this.MnuExite_Click);
            // 
            // 편집EToolStripMenuItem
            // 
            this.편집EToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuCopy,
            this.MnuPaste});
            this.편집EToolStripMenuItem.Name = "편집EToolStripMenuItem";
            this.편집EToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.편집EToolStripMenuItem.Text = "편집(&E)";
            // 
            // MnuCopy
            // 
            this.MnuCopy.Name = "MnuCopy";
            this.MnuCopy.Size = new System.Drawing.Size(180, 22);
            this.MnuCopy.Text = "복사(&C)";
            this.MnuCopy.Click += new System.EventHandler(this.MnuCopy_Click);
            // 
            // MnuPaste
            // 
            this.MnuPaste.Name = "MnuPaste";
            this.MnuPaste.Size = new System.Drawing.Size(180, 22);
            this.MnuPaste.Text = "붙여넣기(&P)";
            this.MnuPaste.Click += new System.EventHandler(this.MnuPaste_Click);
            // 
            // 도움말HToolStripMenuItem
            // 
            this.도움말HToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuAbout});
            this.도움말HToolStripMenuItem.Name = "도움말HToolStripMenuItem";
            this.도움말HToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.도움말HToolStripMenuItem.Text = "도움말(&H)";
            // 
            // MnuAbout
            // 
            this.MnuAbout.Name = "MnuAbout";
            this.MnuAbout.Size = new System.Drawing.Size(180, 22);
            this.MnuAbout.Text = "프로그램정보(&A)";
            this.MnuAbout.Click += new System.EventHandler(this.MnuAbout_Click);
            // 
            // DlgOpenText
            // 
            this.DlgOpenText.FileName = "openFileDialog1";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TxtMain);
            this.Controls.Add(this.MenuScript);
            this.MainMenuStrip = this.MenuScript;
            this.Name = "FrmMain";
            this.Text = "내 메모장";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.MenuScript.ResumeLayout(false);
            this.MenuScript.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip MenuScript;
        private System.Windows.Forms.ToolStripMenuItem 파일ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuNewFile;
        private System.Windows.Forms.ToolStripMenuItem MnuOpen;
        private System.Windows.Forms.ToolStripMenuItem MnuSave;
        private System.Windows.Forms.ToolStripMenuItem MnuExite;
        private System.Windows.Forms.ToolStripMenuItem 편집EToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuCopy;
        private System.Windows.Forms.ToolStripMenuItem MnuPaste;
        private System.Windows.Forms.ToolStripMenuItem 도움말HToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MnuAbout;
        private System.Windows.Forms.RichTextBox TxtMain;
        private System.Windows.Forms.OpenFileDialog DlgOpenText;
        private System.Windows.Forms.SaveFileDialog DlgSaveText;
    }
}

