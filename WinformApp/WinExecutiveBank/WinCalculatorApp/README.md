# Calculator

<kbd>![Calculator](/Capture/WinForm/계산기.PNG "계산기")</kbd>

## 1. 화면 로드

 - 텍스트값 초기화
 - 메모리 기능 버튼 비활성화 
```
private void FrmMain_Load(object sender, EventArgs e)
        {
            TxtExp.Text = TxtResult.Text = "";
            BtnMC.Enabled = BtnMR.Enabled = false;
        }
```
## 2. 입력

 - 0~9 숫자 버튼에 동일 이벤트 적용(BtnNum_Click)
```
private void BtnNum_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            var str = btn.Text; // 0 ~ 9

            TxtResult.Text += str;
        }
```
 - 연산 기호(+,-,×,÷) 버튼에 동일 이벤트 적용(BtnOp_Click)
```
private void BtnOp_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            Saved = double.Parse(TxtResult.Text);
            TxtExp.Text += $"{TxtResult.Text} {btn.Text}";
            op = btn.Text[0]; // (+)\0
            opFlag = true;

            TxtResult.Text = "";
        }
```
## 3. 계산

 - public char op { get; set; }를 이용하여 연산 부호를 받아옴
 - public double Saved { get; set; }를 이용하여 연산 이전 숫자를 받아옴
 - 연산 부호에 따라 계산적용
 
```
private void BtnEqual_Click(object sender, EventArgs e)
        {
            var value = double.Parse(TxtResult.Text);

            switch(op)
            {
                case '+':
                    TxtResult.Text = (Saved + value).ToString();
                    break;
                case '-':
                    TxtResult.Text = (Saved - value).ToString();
                    break;
                case '×':
                    TxtResult.Text = (Saved * value).ToString();
                    break;
                case '÷':
                    TxtResult.Text = (Saved / value).ToString();
                    break;
            }
            TxtExp.Text += $"{value}";
        }
```
## 4. 값 초기화

```
private void BtnC_Click(object sender, EventArgs e)
        {
            TxtResult.Text = TxtExp.Text = "";
            Saved = 0;
            op = '\0';
            opFlag = false;
            PercentFlag = false;
        }
```

## 5. 메모리 저장 및 삭제

 - public double Memory { get; set; } 값 저장
 - 저장 여부 true로 변경
 - memory를 삭제 할 경우 결과 값을 0으로 바꾸고 메모리 불러오기와 삭제를 비활성화
```
private void BtnMS_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtResult.Text)) return;

            Memory = double.Parse(TxtResult.Text);
            BtnMC.Enabled = BtnMR.Enabled = true;
            MemFlag = true;
        }
        
private void BtnMC_Click(object sender, EventArgs e)
        {
            TxtResult.Text = "";
            Memory = 0;
            BtnMR.Enabled = BtnMC.Enabled = false;
        }
```
