
namespace WinChartApp
{
    partial class FrmSub
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ChtScore = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.BtnMerge = new System.Windows.Forms.Button();
            this.BtnSplit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ChtScore)).BeginInit();
            this.SuspendLayout();
            // 
            // ChtScore
            // 
            chartArea3.Name = "ChartArea1";
            this.ChtScore.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.ChtScore.Legends.Add(legend3);
            this.ChtScore.Location = new System.Drawing.Point(12, 12);
            this.ChtScore.Name = "ChtScore";
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.ChtScore.Series.Add(series3);
            this.ChtScore.Size = new System.Drawing.Size(776, 376);
            this.ChtScore.TabIndex = 0;
            this.ChtScore.Text = "chart1";
            // 
            // BtnMerge
            // 
            this.BtnMerge.Location = new System.Drawing.Point(286, 394);
            this.BtnMerge.Name = "BtnMerge";
            this.BtnMerge.Size = new System.Drawing.Size(98, 43);
            this.BtnMerge.TabIndex = 1;
            this.BtnMerge.Text = "합쳐서 그리기";
            this.BtnMerge.UseVisualStyleBackColor = true;
            this.BtnMerge.Click += new System.EventHandler(this.BtnMerge_Click);
            // 
            // BtnSplit
            // 
            this.BtnSplit.Location = new System.Drawing.Point(436, 394);
            this.BtnSplit.Name = "BtnSplit";
            this.BtnSplit.Size = new System.Drawing.Size(98, 43);
            this.BtnSplit.TabIndex = 2;
            this.BtnSplit.Text = "나눠서 그리기";
            this.BtnSplit.UseVisualStyleBackColor = true;
            this.BtnSplit.Click += new System.EventHandler(this.BtnSplit_Click);
            // 
            // FrmSub
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnSplit);
            this.Controls.Add(this.BtnMerge);
            this.Controls.Add(this.ChtScore);
            this.Name = "FrmSub";
            this.Text = "FrmSub";
            this.Load += new System.EventHandler(this.FrmSub_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChtScore)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChtScore;
        private System.Windows.Forms.Button BtnMerge;
        private System.Windows.Forms.Button BtnSplit;
    }
}