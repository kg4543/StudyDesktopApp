# Login

<kbd>![Login](/Capture/WinForm/로그인.PNG "로그인")</kbd>

## 1.로그인 구분
```
private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (TxtId.Text.ToLower() == "admin" && TxtPassward.Text == "123456")
                TxtResult.Text = "로그인 성공";
            else
                TxtResult.Text = "로그인 실패";
        }
```
