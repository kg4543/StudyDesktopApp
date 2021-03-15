
namespace WinChartApp
{
    partial class FrmThirdChart
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.ChtMain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.ChtMain)).BeginInit();
            this.SuspendLayout();
            // 
            // ChtMain
            // 
            chartArea1.Name = "ChartArea1";
            this.ChtMain.ChartAreas.Add(chartArea1);
            this.ChtMain.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.ChtMain.Legends.Add(legend1);
            this.ChtMain.Location = new System.Drawing.Point(0, 0);
            this.ChtMain.Name = "ChtMain";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.ChtMain.Series.Add(series1);
            this.ChtMain.Size = new System.Drawing.Size(800, 450);
            this.ChtMain.TabIndex = 0;
            this.ChtMain.Text = "chart1";
            // 
            // FrmThirdChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ChtMain);
            this.Name = "FrmThirdChart";
            this.Text = "FrmThirdChart";
            this.Load += new System.EventHandler(this.FrmThirdChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ChtMain)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart ChtMain;
    }
}