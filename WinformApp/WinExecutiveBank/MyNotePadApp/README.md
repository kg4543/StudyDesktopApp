# NotePad

<kbd>![NotePad](/Capture/WinForm/메모장.PNG "메모장")</kbd>

## 1. 화면 구성
- MenuStrip을 활용하여 메뉴를 추가
- '메뉴이름(&단축키)'를 통해 단축키 적용
- RichTextBox를 활용해 텍스트화면 구현

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

## 3. 파일작성

- 텍스트 작성 시 IsModify를 true로 바꾸어 수정유무 체크
- file명에 * 를 붙여 저장여부 표기 
```
private void TxtMain_TextChanged(object sender, EventArgs e)
        {
            IsModify = true;
            this.Text = $"{curFileName}* - 내메모장";
        }
```

## 4. 복사 & 붙여넣기

- Clipboard를 활용하여 데이터 복사데이터 저장
- 복사된 데이터를 받아서 문자열로 변경 후 
```
private void MnuCopy_Click(object sender, EventArgs e)
        {
            var contents = ActiveControl as RichTextBox;
            if (contents != null)
            {
                Clipboard.SetDataObject(contents.SelectedText);
                MessageBox.Show(contents.SelectedText);
            }
        }
        
 private void MnuPaste_Click(object sender, EventArgs e)
        {
            var contents = ActiveControl as RichTextBox;
            if (contents != null)
            {
                IDataObject data = Clipboard.GetDataObject();
                contents.SelectedText = data.GetData(DataFormats.Text).ToString();
                IsModify = true;
            }
        }
```

## 5. 파일저장 및 열람

- StreamWriter 와 StreamReader를 통해 파일 저장 및 오픈
- 현재 파일명이 이미 있을 경우 덮어쓰기 아닐 경우 새파일 
- 수정여부를 false로 변경
- 파일명에 * 이 없애기
```
private void MnuSave_Click(object sender, EventArgs e)
        {
            if (curFileName == firstFileName)
            {
                if (DlgSaveText.ShowDialog() == DialogResult.OK)
                    curFileName = DlgSaveText.FileName;
            }
            StreamWriter sw = File.CreateText(curFileName);
            sw.WriteLine(TxtMain.Text);

            IsModify = false;
            sw.Close();
            this.Text = $"{curFileName}";
        }

private void MnuOpen_Click(object sender, EventArgs e)
        {
            ProcessSaveFileBeforeClose();
            DlgOpenText.ShowDialog();

            curFileName = DlgOpenText.FileName;
            this.Text = $"{curFileName}* - 내메모장";

            try
            {
                StreamReader sr = File.OpenText(curFileName);
                TxtMain.Text = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
```

## 6. 종료

- 종료 전 수정 상태에 따라 저장 여부를 묻는 메소드 생성
- 이미 파일명이 있을(저장을 했었던) 경우 파일을 덮어쓰기
- 처음 저장할 경우 파일을 새로 생성
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
## 7. 프로그램 정보

<kbd>![메모장 정보](/Capture/WinForm/메모장%20정보.PNG "메모장 정보")</kbd>

- Main 화면 속성 창의 어플리케이션 정보를 로드하는 정보창
- Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false) 활용 (using System.Reflection;)
```
private void MnuAbout_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("메모장 v1.0입니다.");
            var form = new AboutThis();
            form.ShowDialog();
        }
```
```
namespace MyNotePadApp
{
    partial class AboutThis : Form
    {
        public AboutThis()
        {
            InitializeComponent();
            this.Text = String.Format("{0} 정보", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("버전 {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
        }

        public string AssemblyTitle
        {
           get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }
```
