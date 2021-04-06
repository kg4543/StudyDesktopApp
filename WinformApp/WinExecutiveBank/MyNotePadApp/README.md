# NotePad

<kbd>![NotePad](/Capture/WinForm/메모장.PNG "메모장")</kbd>

<kbd>![메모장 정보](/Capture/WinForm/메모장%20정보.PNG "메모장 정보")</kbd>

## 1. 화면
- MenuStrip을 활용하여 메뉴를 추가
- '메뉴이름(&단축키)'를 통해 단축키 적용

## 2. 파일로드
- 첫 화면 로드 시 파일 명을 "noname.txt"로 읽음
- 화면을 로드하면서 수정상태를 'false'로 지정

```
private const string firstFileName = "noname.txt";

private string curFileName = firstFileName;
        
private void FrmMain_Load(object sender, EventArgs e)
        {
            this.Text = $"{curFileName} - 내 메모장";
            IsModify = false;
            DlgSaveText.Filter = DlgOpenText.Filter = "Text file (*.txt)|*.txt|Log file (*.log)|*.log";
        }
        

```

## 3. 파일수정

## 4. 파일편집 (복사 & 붙여넣기)


## 5. 파일저장 및 열람


## 6. 종료
- 종료 전 수정 상태에 따라 저장 여부를 묻는 메소드 생성
- 이미 파일명이 있을(저장을 했었던) 경우 파일을 덮어쓰기
- 파일명이 noname일(처음 저장할) 경우 파일을 새로 생성

```
private void MnuExite_Click(object sender, EventArgs e)
        {
            ProcessSaveFileBeforeClose();
            Environment.Exit(0);
        }

private void ProcessSaveFileBeforeClose()
        {
            if(IsModify==true)
            {
                DialogResult answer = MessageBox.Show("변경사항이 있습니다. 저장하시겠습니까?" , "저장",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if(answer == DialogResult.Yes)
                {
                    if(curFileName == firstFileName)
                    {
                        if(DlgSaveText.ShowDialog() == DialogResult.OK)
                        {
                            StreamWriter sw = File.CreateText(DlgSaveText.FileName);
                            sw.WriteLine(TxtMain.Text);
                            sw.Close();
                        }
                        else
                        {
                            StreamWriter sw = File.CreateText(curFileName);
                            sw.WriteLine(TxtMain.Text);
                            sw.Close();
                        }
                    }
                }
            }
        }
```
