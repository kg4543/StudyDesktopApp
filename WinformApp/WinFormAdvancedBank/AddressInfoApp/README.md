# AddressInfo

<kbd>![AddressInfo](/Capture/WinForm/주소록.PNG "주소록")</kbd>


## 1. 화면

* DB 데이터 로드
* 이름 텍스트창에 포커스

```
  private void FrmMain_Load(object sender, EventArgs e)
        {
            RefreshData();
            TxtFullName.Focus();
        }
        
    private void RefreshData()
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = "SELECT Idx " +
                               "     , FullName " +
                               "     , Mobile " +
                               "     , Addr " +
                               "  FROM dbo.Address ";

                // SqlCommand & SqlDataReader | Object 사용방법
                // SqlCommand cmd = new SqlCommand( query, conn);
                // SqlDataReader reader = cmd.ExecuteReader();

                // SqlDataAdapter, DataSet
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                DgvAddress.DataSource = ds.Tables[0];
            }
        }
```

## 2. 데이터 선택

```
private void DgvAddress_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var selData = DgvAddress.Rows[e.RowIndex].Cells;

            TxtIdx.Text = selData[0].Value.ToString();
            TxtFullName.Text = selData[1].Value.ToString();
            TxtMobile.Text = selData[2].Value.ToString();
            TxtAddr.Text = selData[3].Value.ToString();
        }
```

## 3. 데이터 입력

 * Insert query문 활용
 * 입력 후 저장된 DB를 다시 로드
 * 입력한 데이터를 저장 후 텍스트박스 값 초기화

```
private void BtnInsert_Click(object sender, EventArgs e)
        {
            int.TryParse(TxtIdx.Text, out int result);
            if (result > 0)
            {
                MessageBox.Show("초기화를 하십시오!");
                return;
            }

            if (string.IsNullOrEmpty(TxtFullName.Text) || string.IsNullOrEmpty(TxtMobile.Text))
            {
                MessageBox.Show("값을 입력하세요.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = $"INSERT INTO dbo.Address " +
                    $"( FullName, " +
                    $" Mobile, " +
                    $" Addr, " +
                    $" RegID, " +
                    $" RegDate) " +
                    $" VALUES " +
                    $"( '{TxtFullName.Text}'," +
                    $" '{TxtMobile.Text.Replace("-","")}'," +
                    $" '{TxtAddr.Text}'," +
                    $" 'admin'," +
                    $" GETDATE() );";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("입력 성공!");
                } else
                {
                    MessageBox.Show("입력 실패!");
                }
                RefreshData();
                ClearInput();
            }
        }
        
 private void ClearInput()
        {
            TxtIdx.Text = TxtFullName.Text = TxtMobile.Text = TxtAddr.Text = "";
            TxtFullName.Focus();
        }
```

## 4. 데이터 수정

```
private void BtnUpdate_Click(object sender, EventArgs e)
        {
            int.TryParse(TxtIdx.Text, out int result);
            if (result == 0)
            {
                MessageBox.Show("데이터를 선택하십시오!");
                return;
            }
            using (SqlConnection conn = new SqlConnection(connString))
            {
                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                string query = $"Update Address set " +
                    $"FullName = '{TxtFullName.Text}', " +
                    $"Mobile = '{TxtMobile.Text.Replace("-", "")}', " +
                    $"Addr = '{TxtAddr.Text}', " +
                    $"ModId = 'admin'," +
                    $"ModDate = GetDate()" +
                    $"Where Idx = {result}";

                SqlCommand cmd = new SqlCommand(query, conn);
                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("수정 성공!");
                }
                else
                {
                    MessageBox.Show("수정 실패!");
                }
                ClearInput();
                RefreshData();
            }
        }
```

## 5. 데이터 삭제

```
private void BtnDelete_Click(object sender, EventArgs e)
        {
            int.TryParse(TxtIdx.Text, out int result);
            if (result == 0)
            {
                MessageBox.Show("데이터를 선택하십시오!");
                return;
            }

            if(MessageBox.Show("삭제하시겠습니까?","삭제",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();

                    string query = $"Delete from Address Where idx = '{result}'";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    if (cmd.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("삭제 성공!");
                    }
                    else
                    {
                        MessageBox.Show("삭제 실패!");
                    }
                    ClearInput();
                    RefreshData();
                }
            }
        }
```

## 6. 입력 후 포커스 이동

```
private void TxtFullName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // 13 =Enter
                TxtMobile.Focus();
        }

        private void TxtMobile_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // 13 =Enter
                TxtAddr.Focus();
        }
```
