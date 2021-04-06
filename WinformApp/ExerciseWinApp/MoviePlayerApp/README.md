# Movie Player

<kbd>![Movie Player](/Capture/WinForm/동영상플레이어.PNG "동영상플레이어")</kbd>

## 1. 파일 선택 및 영상플레이

 - OpenFileDialog를 활용하여 파일 선택
 - WindowsMediaPlayer를 활용하여 영상 플레이
 ```
 private void BtnSelec_Click(object sender, EventArgs e)
        {
            if (DlgSelectFile.ShowDialog() == DialogResult.OK)
            {
                MyPlayer.URL = DlgSelectFile.FileName;
            }
        }
 ```

 - 영상을 플레이 시 큰 화면으로 확장 
```
private void FrmPlayer_Load(object sender, EventArgs e)
        {
            MyPlayer.stretchToFit = true;
        }
```
