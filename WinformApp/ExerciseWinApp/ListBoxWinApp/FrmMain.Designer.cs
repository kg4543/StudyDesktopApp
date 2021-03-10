
namespace ListBoxWinApp
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
            this.LsbGDPCountry = new System.Windows.Forms.ListBox();
            this.LsbGoodCity = new System.Windows.Forms.ListBox();
            this.LsbHappyCountry = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TxtGDPIndex = new System.Windows.Forms.TextBox();
            this.TxtGDPItem = new System.Windows.Forms.TextBox();
            this.TxtGoodIndex = new System.Windows.Forms.TextBox();
            this.TxtGoodItem = new System.Windows.Forms.TextBox();
            this.TxtHappyIndex = new System.Windows.Forms.TextBox();
            this.TxtHappyItem = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnFinish = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LsbGDPCountry
            // 
            this.LsbGDPCountry.FormattingEnabled = true;
            this.LsbGDPCountry.ItemHeight = 12;
            this.LsbGDPCountry.Items.AddRange(new object[] {
            "미국",
            "러시아",
            "중국",
            "독일",
            "프랑스",
            "일본",
            "이스라엘",
            "사우디아라비아",
            "UAE",
            "한국",
            "캐나다"});
            this.LsbGDPCountry.Location = new System.Drawing.Point(146, 89);
            this.LsbGDPCountry.Name = "LsbGDPCountry";
            this.LsbGDPCountry.Size = new System.Drawing.Size(155, 256);
            this.LsbGDPCountry.TabIndex = 0;
            this.LsbGDPCountry.SelectedIndexChanged += new System.EventHandler(this.LsbGDPCountry_SelectedIndexChanged);
            // 
            // LsbGoodCity
            // 
            this.LsbGoodCity.FormattingEnabled = true;
            this.LsbGoodCity.ItemHeight = 12;
            this.LsbGoodCity.Location = new System.Drawing.Point(340, 89);
            this.LsbGoodCity.Name = "LsbGoodCity";
            this.LsbGoodCity.Size = new System.Drawing.Size(155, 256);
            this.LsbGoodCity.TabIndex = 1;
            this.LsbGoodCity.SelectedIndexChanged += new System.EventHandler(this.LsbGoodCity_SelectedIndexChanged);
            // 
            // LsbHappyCountry
            // 
            this.LsbHappyCountry.FormattingEnabled = true;
            this.LsbHappyCountry.ItemHeight = 12;
            this.LsbHappyCountry.Location = new System.Drawing.Point(534, 89);
            this.LsbHappyCountry.Name = "LsbHappyCountry";
            this.LsbHappyCountry.Size = new System.Drawing.Size(155, 256);
            this.LsbHappyCountry.TabIndex = 2;
            this.LsbHappyCountry.SelectedIndexChanged += new System.EventHandler(this.LsbHappyCountry_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(144, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "GDP 국가별 순위";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(338, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "살기 좋은 도시";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 354);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "Selected Index";
            // 
            // TxtGDPIndex
            // 
            this.TxtGDPIndex.Location = new System.Drawing.Point(146, 351);
            this.TxtGDPIndex.Name = "TxtGDPIndex";
            this.TxtGDPIndex.Size = new System.Drawing.Size(155, 21);
            this.TxtGDPIndex.TabIndex = 6;
            // 
            // TxtGDPItem
            // 
            this.TxtGDPItem.Location = new System.Drawing.Point(146, 378);
            this.TxtGDPItem.Name = "TxtGDPItem";
            this.TxtGDPItem.Size = new System.Drawing.Size(155, 21);
            this.TxtGDPItem.TabIndex = 7;
            // 
            // TxtGoodIndex
            // 
            this.TxtGoodIndex.Location = new System.Drawing.Point(340, 351);
            this.TxtGoodIndex.Name = "TxtGoodIndex";
            this.TxtGoodIndex.Size = new System.Drawing.Size(155, 21);
            this.TxtGoodIndex.TabIndex = 8;
            // 
            // TxtGoodItem
            // 
            this.TxtGoodItem.Location = new System.Drawing.Point(340, 378);
            this.TxtGoodItem.Name = "TxtGoodItem";
            this.TxtGoodItem.Size = new System.Drawing.Size(155, 21);
            this.TxtGoodItem.TabIndex = 9;
            // 
            // TxtHappyIndex
            // 
            this.TxtHappyIndex.Location = new System.Drawing.Point(534, 351);
            this.TxtHappyIndex.Name = "TxtHappyIndex";
            this.TxtHappyIndex.Size = new System.Drawing.Size(155, 21);
            this.TxtHappyIndex.TabIndex = 10;
            // 
            // TxtHappyItem
            // 
            this.TxtHappyItem.Location = new System.Drawing.Point(534, 378);
            this.TxtHappyItem.Name = "TxtHappyItem";
            this.TxtHappyItem.Size = new System.Drawing.Size(155, 21);
            this.TxtHappyItem.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 381);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(82, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "Selected Item";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(532, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "행복한 나라";
            // 
            // BtnFinish
            // 
            this.BtnFinish.Location = new System.Drawing.Point(146, 416);
            this.BtnFinish.Name = "BtnFinish";
            this.BtnFinish.Size = new System.Drawing.Size(543, 23);
            this.BtnFinish.TabIndex = 12;
            this.BtnFinish.Text = "초기화";
            this.BtnFinish.UseVisualStyleBackColor = true;
            this.BtnFinish.Click += new System.EventHandler(this.BtnFinish_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 453);
            this.Controls.Add(this.BtnFinish);
            this.Controls.Add(this.TxtHappyItem);
            this.Controls.Add(this.TxtHappyIndex);
            this.Controls.Add(this.TxtGoodItem);
            this.Controls.Add(this.TxtGoodIndex);
            this.Controls.Add(this.TxtGDPItem);
            this.Controls.Add(this.TxtGDPIndex);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LsbHappyCountry);
            this.Controls.Add(this.LsbGoodCity);
            this.Controls.Add(this.LsbGDPCountry);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListboxTest";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LsbGDPCountry;
        private System.Windows.Forms.ListBox LsbGoodCity;
        private System.Windows.Forms.ListBox LsbHappyCountry;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TxtGDPIndex;
        private System.Windows.Forms.TextBox TxtGDPItem;
        private System.Windows.Forms.TextBox TxtGoodIndex;
        private System.Windows.Forms.TextBox TxtGoodItem;
        private System.Windows.Forms.TextBox TxtHappyIndex;
        private System.Windows.Forms.TextBox TxtHappyItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnFinish;
    }
}

