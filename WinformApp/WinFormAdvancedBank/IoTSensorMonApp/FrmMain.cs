﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace IoTSensorMonApp
{
    public partial class FrmMain : Form
    {
        private double xCount = 200;

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            // ComboBox 초기화
            foreach (var port in SerialPort.GetPortNames())
            {
                CboSerialPort.Items.Add(port);
            }
            CboSerialPort.Text = "select Port";

            // IoT 장비에서 받을 값의 범위
            PrbPhotoRegister.Minimum = 0;
            PrbPhotoRegister.Maximum = 1023;
            PrbPhotoRegister.Value = 0;

            // 차트모양 초기화
            InitChartStyle();

            // Display 버튼 초기화
            BtnDisplay.BackColor = Color.IndianRed;
            BtnDisplay.ForeColor = Color.WhiteSmoke;
            BtnDisplay.Text = "NONE";
            BtnDisplay.Font = new Font("맑은 고딕", 14, FontStyle.Bold);

            // 나머지 초기화
            LblConnectTime.Text = "Connection Time : ";
            TxtSensorNum.Text = "0";
            BtnConnect.Enabled = BtnDisconnect.Enabled = false;
        }

        /// <summary>
        /// 차트 초기설정
        /// </summary>
        private void InitChartStyle()
        {
            ChtPhotoRegisters.ChartAreas[0].BackColor = Color.Black;
            ChtPhotoRegisters.ChartAreas[0].AxisX.Minimum = 0;
            ChtPhotoRegisters.ChartAreas[0].AxisX.Maximum = xCount;
            ChtPhotoRegisters.ChartAreas[0].AxisX.Interval = xCount / 4;
            ChtPhotoRegisters.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.WhiteSmoke;
            ChtPhotoRegisters.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            ChtPhotoRegisters.ChartAreas[0].AxisY.Minimum = 0;
            ChtPhotoRegisters.ChartAreas[0].AxisY.Maximum = 1024;
            ChtPhotoRegisters.ChartAreas[0].AxisY.Interval = xCount;
            ChtPhotoRegisters.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.WhiteSmoke;
            ChtPhotoRegisters.ChartAreas[0].AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            ChtPhotoRegisters.ChartAreas[0].CursorX.AutoScroll = true;
            ChtPhotoRegisters.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            ChtPhotoRegisters.ChartAreas[0].AxisX.ScrollBar.ButtonStyle = ScrollBarButtonStyles.SmallScroll;
            ChtPhotoRegisters.ChartAreas[0].AxisX.ScrollBar.ButtonColor = Color.LightSteelBlue;

            ChtPhotoRegisters.Series.Clear(); //Series 1 값 지움
            ChtPhotoRegisters.Series.Add("PhotoValie");
            ChtPhotoRegisters.Series[0].ChartType = SeriesChartType.Line;
            ChtPhotoRegisters.Series[0].Color = Color.Yellow;
            ChtPhotoRegisters.Series[0].BorderWidth = 3;

            //범례삭제
            if (ChtPhotoRegisters.Legends.Count > 0)
            {
                for (int i = 0; i < ChtPhotoRegisters.Legends.Count; i++)
                {
                    ChtPhotoRegisters.Legends.RemoveAt(i);
                }
            }

        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            // 실제 작업시 작성
        }

        private void BtnDisconnect_Click(object sender, EventArgs e)
        {
            // 실제 작업시 작성
        }

        private Timer timerSimul = new Timer();
        private Random randPhoto = new Random();
        private bool IsSimulation = false;
        private List<SensorData> sensors = new List<SensorData>(); // 차트,리스트박스에 출력

        private void MnunBeginSimulation_Click(object sender, EventArgs e)
        {
            //시뮬레이션 시작
            IsSimulation = true;
            timerSimul.Enabled = true;
            timerSimul.Interval = 1000;
            timerSimul.Tick += TimerSimul_Tick;
            timerSimul.Start();
        }

        private void TimerSimul_Tick(object sender, EventArgs e)
        {
            int value = randPhoto.Next(1, 1023); //1~1023 사이의 값
            ShowSensorValue(value.ToString());
        }

        private void ShowSensorValue(string value)
        {
            int.TryParse(value, out int val);

            var currentTime = DateTime.Now;
            SensorData data = new SensorData(currentTime, val, IsSimulation);
            sensors.Add(data);

            //센서 값 갯수
            TxtSensorNum.Text = sensors.Count.ToString(); // or $"{sensors.Count}"
            //프로그래스바 현재값 출력
            PrbPhotoRegister.Value = val;
            //리스트박스에 데이터 출력
            var item = $"{currentTime.ToString("yyyy-MM-dd HH:mm:ss")}\t{val}";
            LsbPhotoRegisters.Items.Add(item);
            LsbPhotoRegisters.SelectedIndex = LsbPhotoRegisters.Items.Count - 1; //스크롤처리
            //차트에 데이터 출력
            ChtPhotoRegisters.Series[0].Points.Add(val);
        }

        private void MnuEndSimulation_Click(object sender, EventArgs e)
        {
            //시뮬레이션 종료
            IsSimulation = false;
            timerSimul.Stop();
        }

        private void MnuExit_Click(object sender, EventArgs e)
        {
            if (IsSimulation)
            {
                MessageBox.Show("시뮬레이션을 멈춘 후 종료하세요!","종료",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            Environment.Exit(0);
        }
    }
}
