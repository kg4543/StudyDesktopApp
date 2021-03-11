using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace AlarmClockApp
{
    public partial class FrmAlarm : Form
    {
        private DateTime SetDay;
        private DateTime SetTime;
        private bool IsSetAlarm;
        WindowsMediaPlayer mediaPlayer;

        public FrmAlarm()
        {
            InitializeComponent();
        }

        private void FrmAlarm_Load(object sender, EventArgs e)
        {
            mediaPlayer = new WindowsMediaPlayer();
            LblAlarm.ForeColor = Color.Gray;

            LblDate.Text = LblTime.Text = ""; // 시작 시 글자를 지워줌

            DtpAlarmTime.Format = DateTimePickerFormat.Custom;
            DtpAlarmTime.CustomFormat = "hh:mm:ss"; // 디자인에서 설정 가능
            DtpAlarmTime.ShowUpDown = true;

            MyTimer.Interval = 1000; // 1초
            MyTimer.Tick += MyTimer_Tick;
            MyTimer.Enabled = true;
            MyTimer.Start();

            TabClock.SelectedTab = TapDigitalClock;
        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            DateTime curDate = DateTime.Now;
            LblDate.Text = curDate.ToShortDateString();
            // LblTime.Text = curDate.ToLongTimeString(); 오전.오후 표기
            LblTime.Text = curDate.ToString("hh:mm:ss");

            if (IsSetAlarm == true) //알람 설정 시
            {
                // 알람시간 == 현재시간
                if (SetDay == DateTime.Today && 
                    SetTime.Hour == curDate.Hour &&
                    SetTime.Minute == curDate.Minute &&
                    SetTime.Second == curDate.Second)
                {
                    // IsSetAlarm = false;
                    BtnRelease_Click(sender, e);
                    mediaPlayer.URL = @".\Medias\alarm.mp3";
                    mediaPlayer.controls.play();
                    MessageBox.Show("Alarm!!","Alarm",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
        }

        private void BtnSet_Click(object sender, EventArgs e)
        {
            SetDay = DateTime.Parse(DtpAlarmDate.Text);
            SetTime = DateTime.Parse(DtpAlarmTime.Text);

            LblAlarm.Text = $"Alarm : {SetDay.ToShortDateString()} {SetTime.ToString("hh:mm:ss")}";
            LblAlarm.ForeColor = Color.Red;

            TabClock.SelectedTab = TapDigitalClock;
            IsSetAlarm = true;
        }

        private void BtnRelease_Click(object sender, EventArgs e)
        {
            IsSetAlarm = false;
            LblAlarm.Text = "Alarm : ";
            LblAlarm.ForeColor = Color.Gray;
            TabClock.SelectedTab = TapDigitalClock;
        }
    }
}
