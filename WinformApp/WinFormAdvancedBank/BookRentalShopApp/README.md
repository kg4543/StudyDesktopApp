# Main

<kbd>![Main](/Capture/BookRental/Main.PNG "Main")</kbd>

### 1.MainForm 로드 시 로그인창을 불러오기
 
 * (showdialog)를 사용하여 로그인을 해야 Main창 활성화
```
private void FrmMain_Shown(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
        }
```

### 2. 각 메뉴창 클릭 시 해당 창을  로드

```
private void MnuDivCode_Click(object sender, EventArgs e)
        {
            FrmDivCode frm = new FrmDivCode();
            InitChildForm(frm, "장르 관리");
        }

        private void MnuMember_Click(object sender, EventArgs e)
        {
            FrmMember frm = new FrmMember();
            InitChildForm(frm, "회원관리");
        }

        private void MnuBooks_Click(object sender, EventArgs e)
        {
            FrmBooks frm = new FrmBooks();
            InitChildForm(frm, "도서 관리");
        }
        private void MnuRental_Click(object sender, EventArgs e)
        {
            FrmRental frm = new FrmRental();
            InitChildForm(frm, "대여 관리");
        }
```

-------------------------------
# 로그인

<kbd>![Login](/Capture/BookRental/Login.PNG "Login")</kbd>

### 1. 입력값의 null 값 체크
```
if (string.IsNullOrEmpty(TxtUserID.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MetroMessageBox.Show(this,"아이디/패스워드를 입력하세요!","오류",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
```

### 2. 회원 DB를 불러와 입력한 ID와 Password 비교

* strUserID에 DB에서 입력한 UserID와 password가 매칭되는 UserID를 호출시킴
* 매칭되지 않아 null 값일 경우 로그인 
```
// 입력한 userID와 password가 일치하는 UserID를 받아와 비교
                    var query = "select userID from membertbl " +
                                " where userID = @userID " +
                                " and passwords = @passwords ";
                                
......

strUserId = reader["UserID"] != null ? reader["userID"].ToString() : "";

if (string.IsNullOrEmpty(strUserId))
                    {
                        MetroMessageBox.Show(this, "접속실패", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
```

### 2. Sql Injectrion 

* Sql Injectrion : 입력창에 쿼리문을 입력하여 DB 데이터를 조작
* 'SqlParameter'를 활용하여 'SqlCommand'를 로드하여 Sql Injectrion 예방
* Helper-Common에서 DB연결 및 'SqlCommand' 예방
```
 // Sql Command 생성
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Sql injection 예방
                    SqlParameter pUserID = new SqlParameter("@userID", SqlDbType.VarChar, 20);
                    pUserID.Value = TxtUserID.Text;
                    cmd.Parameters.Add(pUserID);

                    SqlParameter pPassword = new SqlParameter("@Passwords", SqlDbType.VarChar, 20);
                    pPassword.Value = TxtPassword.Text;
                    cmd.Parameters.Add(pPassword);
```

* Sql Injection을 예방하기위헤 ',--,; 을 특수문자 및 빈칸으로 변경 

#### Helper Folder - Common
```
public static class Common
    {
        ......
        
        internal static string ReplaceCmdText(string strSource)
        {
            var result = strSource.Replace("'","＇");
            result = result.Replace("--","");
            result = result.Replace(";","");

            return result;
        }
    }
```

### 3. ip주소와 접속시간을 DB에 update

```
else
                    {
                        var updateQuery = $@"update membertbl set 
                                            lastLoginDt = GETDATE(),
                                            loginIpAddr = '{Helper.Common.GetLocalIp()}' 
                                            where userID = '{strUserId}'";
                        cmd.CommandText = updateQuery;
                        cmd.ExecuteNonQuery();
                        MetroMessageBox.Show(this, "접속성공", "로그인 성공", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        this.Close();
                    }
```

#### Helper Folder - Common

```
public static class Common
    {
        public static string ConnString = "Data Source=127.0.0.1;" +
                                          "Initial Catalog=bookrentalshop;" +
                                          "Persist Security Info=True;" +
                                          "User ID = sa; PassWord=mssql_p@ssw0rd!";
        public static string LoginUserId = string.Empty;

        /// <summary>
        /// Ip주소 받아오기
        /// </summary>
        /// <returns></returns>
        internal static string GetLocalIp()
        {
            string localIP = "";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach  (IPAddress ip in host.AddressList)
            {
                localIP = ip.ToString();
                break;
            }
            return localIP;
        }
```

-------------------------------
# 장르관리

<kbd>![Division](/Capture/BookRental/Division.PNG "Division")</kbd>

-------------------------------
# 회원관리

<kbd>![Member](/Capture/BookRental/Member.PNG "Member")</kbd>

-------------------------------
# 도서관리

<kbd>![Book](/Capture/BookRental/Book.PNG "Book")</kbd>

-------------------------------
# 대여관리

<kbd>![Rental](/Capture/BookRental/Rental.PNG "Rental")</kbd>

-------------------------------
# 종료

<kbd>![Exit](/Capture/BookRental/Exit.PNG "Exit")</kbd>

메뉴 종료 버튼이나 window 종료 버튼 시 활성화

```
private void MnuExit_Click(object sender, EventArgs e)
        {
            if (MetroMessageBox.Show(this, "종료하시겠습니까?", "종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Environment.Exit(0);
            }
        }

private void FrmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MetroMessageBox.Show(this, "종료하시겠습니까?", "종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                e.Cancel = false;
                Environment.Exit(0);
            }
            else
                e.Cancel = true;
        }
```
-------------------------------
