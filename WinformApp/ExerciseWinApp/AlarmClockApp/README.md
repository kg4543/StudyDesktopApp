# Alarm

<kbd>![SetAlarm](/Capture/WinForm/알람설정.PNG "알람설정")</kbd>
<kbd>![Alarm](/Capture/WinForm/알람.PNG "알람")</kbd>

## 1. 날짜 & 시간 표기

- DateTimePicker를 활용하여 날짜 및 시간을 표현
- DateTimePickerFormat.Custom;을 활용하여 알람시간 디자인
- WindowsMediaPlayer mediaPlayer;를 활용하여 알람소리 설정 (using WMPLib;)
- MyTimer.Tick으로 시간경과 측정

```
  private DateTime SetDay;
  private DateTime SetTime;
  private bool IsSetAlarm;
  WindowsMediaPlayer mediaPlayer;

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
```

## 2. 알람 설정

```
private void BtnSet_Click(object sender, EventArgs e)
        {
            SetDay = DateTime.Parse(DtpAlarmDate.Text);
            SetTime = DateTime.Parse(DtpAlarmTime.Text);

            LblAlarm.Text = $"Alarm : {SetDay.ToShortDateString()} {SetTime.ToString("hh:mm:ss")}";
            LblAlarm.ForeColor = Color.Red;

            TabClock.SelectedTab = TapDigitalClock;
            IsSetAlarm = true;
        }
```

## 3. 알람 설정 해제

```
private void BtnRelease_Click(object sender, EventArgs e)
        {
            IsSetAlarm = false;
            LblAlarm.Text = "Alarm : ";
            LblAlarm.ForeColor = Color.Gray;
            TabClock.SelectedTab = TapDigitalClock;
        }
```
