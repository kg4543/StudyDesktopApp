# Main

<kbd>![Main](/Capture/BookRental/Main.PNG "Main")</kbd>

## 1. MainForm 로드 시 로그인창을 불러오기
 
 * (showdialog)를 사용하여 로그인을 해야 Main창 활성화
```
private void FrmMain_Shown(object sender, EventArgs e)
        {
            FrmLogin frm = new FrmLogin();
            frm.ShowDialog();
        }
```

## 2. 각 메뉴창 클릭 시 해당 창을  로드

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

## 1. 입력값의 null 값 체크
```
if (string.IsNullOrEmpty(TxtUserID.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MetroMessageBox.Show(this,"아이디/패스워드를 입력하세요!","오류",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
```

## 2. 회원 DB를 불러와 입력한 ID와 Password 비교

* strUserID에 DB에서 입력한 UserID와 password가 매칭되는 UserID를 호출시킴
* 매칭되지 않아 null 값일 경우 로그인 
* query문 where 절에 권한 여부를 추가하여 관리자만 로그인할 수 있도록 지정 가능

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

## 3. Sql Injectrion 

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

## 4. ip주소와 접속시간을 DB에 update

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

### Helper Folder - Common

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

## 1. DB 데이터 로드 및 상세 데이터 초기화

* 데이터 그리드 뷰에서 DB 데이터를 로드
* 모듈화시켜 데이터에 변동이 생길 시 실시간으로 로드시킴

```
private void FrmDivCode_Load(object sender, EventArgs e)
        {
            RefreshData();
            ClearInputs();
        }
 
private void RefreshData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "Select [Division]" +
                                "      ,[Names]" +
                                "  from [dbo].[divtbl]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "divtbl");

                    DgvData.DataSource = ds;
                    DgvData.DataMember = "divtbl";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
```
* 상세 그룹에 입력 데이터 값을 빈값으로 로드
* 모듈화 시켜 저장 및 삭제 후에도 정보를 초기화 시켜줌
* 신규 데이터 생성 전 입력 데이터 값 초기화 
```
private void ClearInputs()
        {
            TxtDivision.Text = TxtDivision.Text = "";
            TxtName.Text = TxtName.Text = "";
            TxtDivision.ReadOnly = false;
            IsNew = true;
        }
```

## 2. 데이터 선택 및 로드

* 상세 입력 데이터에 DB에 선택한 값을 표기
* isnew = false로 변경하여 '저장' 시 선택된 값이 변경

```
private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) //선택된 값이 존재(rowindex : 0~)
            {
                var selData = DgvData.Rows[e.RowIndex];
                TxtDivision.Text = selData.Cells[0].Value.ToString();
                TxtName.Text = selData.Cells[1].Value.ToString();
                TxtDivision.ReadOnly = true;
                IsNew = false;
            }
        }
```

## 3. 입력 데이터 값 검증

* 데이터 저장(신규 및 업데이트) 전 필수 값들이 null 값이 아닌지 체크

```
private bool CheckValidation()
        {
            if (string.IsNullOrEmpty(TxtDivision.Text) || string.IsNullOrEmpty(TxtName.Text))
            {
                MetroMessageBox.Show(this, "빈값은 처리할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
```

## 4. 데이터 저장 (신규 생성 및 업데이트)

* isnew가 true일 경우 'insert'문으로 신규 데이터 생성
* isnew가 false일 경우 'update'문으로 기존 데이터 변경
* 데이터 저장 후 RefreshData()로  DB데이터를 최신을로 로드
* ClearInput()으로 상세 입력 데이터 초기화 및 isnew = true로 변경

```
private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }
        
private void BtnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidation() == false) return;

            SaveDate();
            RefreshData();
            ClearInputs();
        }
```

```
private void SaveDate()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "";

                    if (IsNew == true)
                    {
                        query = "Insert into dbo.divtbl " +
                            " values " +
                            " (@Division, @Names) ";
                    }
                    else
                    {
                        query = "UPDATE [dbo].[divtbl] " +
                            " SET [Names] = @Names " +
                            " WHERE [Division] = @Division";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    SqlParameter pNames = new SqlParameter("@Names", SqlDbType.NVarChar, 45);
                    pNames.Value = TxtName.Text;
                    cmd.Parameters.Add(pNames);

                    SqlParameter pDivision = new SqlParameter("@Division", SqlDbType.NVarChar, 45);
                    pDivision.Value = TxtDivision.Text;
                    cmd.Parameters.Add(pDivision);

                    var result = cmd.ExecuteNonQuery();
                    if (result == 1)
                    {
                        MetroMessageBox.Show(this, "저장 성공", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MetroMessageBox.Show(this, "저장 실패", "저장", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
```

## 5. 데이터 삭제

* 선택된 값을 DB에서 삭제시킴

```
private void BtnDelete_Click(object sender, EventArgs e)
        {
            // Validation 유효성 체크
            if (CheckValidation() == false) return;

            DeleteDate();
            RefreshData();
            ClearInputs();
        }

private void DeleteDate()
        {
            try
            {
                if (MetroMessageBox.Show(this, "삭제하시겠습니까?", "삭제",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();

                        var query = "";

                        query = "Delete From [dbo].divtbl" +
                                " WHERE [Division] = @Division";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        SqlParameter pDivision = new SqlParameter("@Division", SqlDbType.NVarChar, 45);
                        pDivision.Value = TxtDivision.Text;
                        cmd.Parameters.Add(pDivision);

                        var result = cmd.ExecuteNonQuery();
                        if (result == 1)
                        {
                            MetroMessageBox.Show(this, "삭제 성공", "저장", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MetroMessageBox.Show(this, "삭제 실패", "저장", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
```
-------------------------------
# 회원관리

<kbd>![Member](/Capture/BookRental/Member.PNG "Member")</kbd>

## 장르관리와 유사
* DB 로드 및 입력 데이터 초기화
* DB 값을 선택하여 상세 값 로드
* 필수 값 입력유무 검증
* isnew를 사용하여 데이터 신규 생성 및 기존 데이터 수정
* 선택 데이터 삭제
* 내부 데이터 값만 변경

```
private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) //선택된 값이 존재하면
            {
                var selData = DgvData.Rows[e.RowIndex];
                TxtIdx.Text = selData.Cells[0].Value.ToString();
                TxtName.Text = selData.Cells[1].Value.ToString();
                CboLevels.SelectedItem = selData.Cells[2].Value.ToString();
                TxtAddr.Text = selData.Cells[3].Value.ToString();
                TxtMobile.Text = selData.Cells[4].Value.ToString();
                TxtEmail.Text = selData.Cells[5].Value.ToString();
                TxtUserID.Text = selData.Cells[6].Value.ToString();
                TxtIdx.ReadOnly = true;
                IsNew = false;
            }
        }
```


  
-------------------------------
# 도서관리

<kbd>![Book](/Capture/BookRental/Book.PNG "Book")</kbd>

## 장르관리와 유사
* DB 로드 및 입력 데이터 초기화
* DB 값을 선택하여 상세 값 로드
* 필수 값 입력유무 검증
* isnew를 사용하여 데이터 신규 생성 및 기존 데이터 수정
* 선택 데이터 삭제
* 내부 데이터 값만 변경 (날짜 데이터 형식 유의)

```
private void FrmDivCode_Load(object sender, EventArgs e)
        {
            IsNew = true; //신규인지 구분(초기화)
            InitCboData(); // 콤보박스 들어가는 데이터 조회
            RefreshData(); //테이블 조회
            ClearInputs();

            DtpReleaseDate.CustomFormat = "yyyy-MM-dd";
            DtpReleaseDate.Format = DateTimePickerFormat.Custom;
        }
        
private void AsignToControls(DataGridViewRow selData)
        {
            TxtIdx.Text = selData.Cells[0].Value.ToString();
            TxtAuthor.Text = selData.Cells[1].Value.ToString();
            CboDivision.SelectedValue = selData.Cells[2].Value.ToString();
            DtpReleaseDate.Value = (DateTime)selData.Cells[5].Value;
            TxtNames.Text = selData.Cells[4].Value.ToString();
            TxtISBN.Text = selData.Cells[6].Value.ToString();
            TxtPrice.Text = selData.Cells[7].Value.ToString();
            TxtDescriptions.Text = selData.Cells[8].Value.ToString();
            TxtIdx.ReadOnly = true;
        }
```
-------------------------------
# 대여관리

<kbd>![Rental](/Capture/BookRental/Rental.PNG "Rental")</kbd>

## 1. DB 데이터 로드 및 입력 데이터 초기화

* Outer Join을 통해 대여 DB 로드
* 대여 상태(rentalState) 값(R,T)을 (대여,반납)으로 로드
* Idx값을 수정하지 못 하도록 readonly로 고정
* 입력 데이터를 초기화하면서 회원과 도서를 검색할 수 있도록 검색 버튼  
  
```
private void FrmDivCode_Load(object sender, EventArgs e)
        {
            IsNew = true; //신규인지 구분(초기화)
            InitCboData(); // 콤보박스 들어가는 데이터 조회
            RefreshData(); //테이블 조회
            ClearInputs();

            DtpRentalDate.CustomFormat = "yyyy-MM-dd";
            DtpRentalDate.Format = DateTimePickerFormat.Custom;
        }
        
 private void InitCboData() 
        {
            try
            {
                var temp = new Dictionary<string, string>();
                temp.Add("R", "대여");
                temp.Add("T", "반납");

                CboRentalState.DataSource = new BindingSource(temp, null);
                CboRentalState.DisplayMember = "Value";
                CboRentalState.ValueMember = "Key";
                CboRentalState.SelectedIndex = -1;
            }
            catch { }
        }
 
 private void RefreshData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = @"SELECT r.Idx
                                      ,r.memberIdx
	                                  ,m.Names as memberName
                                      ,r.bookIdx
	                                  ,b.Names as bookName
                                      ,r.rentalDate
                                      ,r.returnDate
	                                  ,r.rentalState
                                      ,case r.rentalState 
	                                      when 'R' then '대여중'
	                                      when 'T' then '반납' 
	                                      else '상태없음'
	                                   end as StateDesc
                                  FROM dbo.rentaltbl as r
                                 INNER JOIN dbo.membertbl as m
                                    ON r.memberIdx = m.Idx
                                 INNER JOIN dbo.bookstbl as b
                                    ON r.bookIdx = b.Idx;";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn); // 가져올 데이터베이스를 변환
                    DataSet ds = new DataSet(); // 가상의 데이터베이스
                    adapter.Fill(ds, "rentaltbl");

                    DgvData.DataSource = ds;
                    DgvData.DataMember = "rentaltbl";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            var column = DgvData.Columns[1]; //MemberIdx column
            column.Visible = false;
            column = DgvData.Columns[3]; //BookIdx column
            column.Visible = false;
         }
         
private void ClearInputs()
        {
            selMemberIdx = 0;
            selMemberName = "";
            selBookIdx = 0;
            selBookName = "";
            TxtBookName.Text = TxtMemberName.Text = "";
            TxtIdx.Text = "";
            DtpRentalDate.Value = DateTime.Now;
            TxtReturnDate.Text = "";
            CboRentalState.SelectedIndex = -1;

            IsNew = true;
            BtnSearchMember.Enabled = BtnSearchBook.Enabled = true;
            DtpRentalDate.Enabled = true;
        }
```
## 2. 데이터 선택 및 로드

* 검색 창 및 날짜  선택 창 비활성화

```
private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) //선택된 값이 존재하면
            {
                var selData = DgvData.Rows[e.RowIndex];
                AsignToControls(selData);
                
                IsNew = false;
                BtnSearchMember.Enabled = BtnSearchBook.Enabled = false;
                DtpRentalDate.Enabled = false;
            }
        }
 
 private void AsignToControls(DataGridViewRow selData)
        {
            TxtIdx.Text = selData.Cells[0].Value.ToString();
            selMemberIdx = (int)selData.Cells[1].Value;
            Debug.WriteLine($">>>> selMemberIdx : {selMemberIdx}");
            TxtMemberName.Text = selData.Cells[2].Value.ToString();
            selBookIdx = (int)selData.Cells[3].Value;
            Debug.WriteLine($">>>> selBookIdx : {selBookIdx}");
            TxtBookName.Text = selData.Cells[4].Value.ToString();
            DtpRentalDate.Value = (DateTime)selData.Cells[5].Value;
            TxtReturnDate.Text = selData.Cells[6].Value == null ? "" : selData.Cells[6].Value.ToString();
            CboRentalState.SelectedValue = selData.Cells[7].Value;

            TxtIdx.ReadOnly = true;
        }
```

## 3. 데이터 값 검증

```
private bool CheckValidation()
        {
            if ( string.IsNullOrEmpty(TxtMemberName.Text) || string.IsNullOrEmpty(TxtBookName.Text) ||
                DtpRentalDate.Value == null)
            {
                MetroMessageBox.Show(this, "빈값은 처리할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
```

## 4. 데이터 저장

* 신규일 때는 대여
* 기존 데이터일 때는 반납
* Isnew로 구분

```
private void SaveData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    var query = "";
                    
                    if (IsNew == true)
                    {
                        query = @"INSERT into [dbo].[rentaltbl]
                                                   ([memberIdx]
                                                   ,[bookIdx]
                                                   ,[rentalDate]
                                                   ,[rentalState])
                                             VALUES
                                                   (@memberIdx
                                                   ,@bookIdx
                                                   ,@rentalDate
                                                   ,@rentalState)";
                    }
                    else
                    {
                        query = @"UPDATE [dbo].[rentaltbl]
                                       SET [returnDate] = case @rentalState
                                                          when 'T' then GETDATE()
                                                          when 'R' then null end
                                          ,[rentalState] = @rentalState
                                     WHERE Idx = @Idx ";
                    }

                    cmd.CommandText = query;
         ......
        }
```

## 5. 회원 및 도서 검색

<kbd>![대여_회원](/Capture/BookRental/대여_회원.PNG "대여_회원")</kbd>

* 전역 변수 SelIdx와 selName을 활용
* FrmMemvberPopup에서 선택한 DB 값의 idx와 Name을 전역변수에 호출하여 FrmRental에서 사용
* 도서 검색은 회원 검색과 방식 동일

```
private void BtnSearchMember_Click(object sender, EventArgs e)
        {
            FrmMemberPopup frm = new FrmMemberPopup();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                selMemberIdx = frm.SelIdx;
                TxtMemberName.Text = selMemberName = frm.SelName;
            }
        }

public partial class FrmMemberPopup : MetroForm
 {
 
  public int SelIdx { get; set; }

  public string SelName { get; set; }
        
    private void FrmDivCode_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
        private void RefreshData()
        {
         ......
        }

private void BtnSelect_Click(object sender, EventArgs e)
        {
            if (DgvData.SelectedRows.Count == 0)
            {
                MetroMessageBox.Show(this, "데이터를 선택하세요!", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (DgvData.SelectedRows.Count > 1)
            {
                MetroMessageBox.Show(this, "데이터를 한 개만 선택하세요!", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                SelIdx = (int)DgvData.SelectedRows[0].Cells[0].Value;
                SelName = DgvData.SelectedRows[0].Cells[1].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
```
-------------------------------
# 종료

<kbd>![Exit](/Capture/BookRental/Exit.PNG "Exit")</kbd>

## 1. 메뉴 종료 버튼이나 window 종료 버튼 시 활성화

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
