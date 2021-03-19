using MetroFramework.Forms;
using MetroFramework;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BookRentalShopApp
{
    public partial class FrmBooks : MetroForm
    {
        #region 전역변수 영역
        private bool IsNew { get; set; } // false (수정) / true (신규)
        #endregion

        #region 이벤트 영역
        public FrmBooks()
        {
            InitializeComponent();
        }

        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            IsNew = true; //신규인지 구분(초기화)
            InitCboData(); // 콤보박스 들어가는 데이터 조회
            RefreshData(); //테이블 조회
            ClearInputs();

            DtpReleaseDate.CustomFormat = "yyyy-MM-dd";
            DtpReleaseDate.Format = DateTimePickerFormat.Custom;
        }

        private void FrmDivCode_Resize(object sender, EventArgs e)
        {
            DgvData.Height = this.ClientRectangle.Height - 90;
            GrbDetail.Height = this.ClientRectangle.Height - 90;
        }
       
        private void DgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1) //선택된 값이 존재하면
            {
                var selData = DgvData.Rows[e.RowIndex];

                AsignToControls(selData);
                
                IsNew = false;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            // Validation 유효성 체크
            if (CheckValidation() == false) return;

            DeleteDate();
            RefreshData();
            ClearInputs();
        }

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
        #endregion

        #region 커스템 메소드 영역
        /// <summary>
        /// 콤보박스에 데이터베이스에 있는 데이터 받아서 넣기
        /// </summary>
        private void InitCboData() 
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                   if (conn.State == ConnectionState.Closed) conn.Open();
                    var query = "select Division,Names from dbo.divtbl";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    var temp = new Dictionary<string, string>();
                    while (reader.Read())
                    {
                        temp.Add(reader[0].ToString(), reader[1].ToString()); // B001, 공포스릴러
                    }
                    CboDivision.DataSource = new BindingSource(temp, null);
                    CboDivision.DisplayMember = "Value";
                    CboDivision.ValueMember = "Key";
                    CboDivision.SelectedIndex = -1;
                }
            }
            catch { }
        }

        /// <summary>
        /// 데이터 값 선택 시 load
        /// </summary>
        /// <param name="selData"></param>
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

        /// <summary>
        /// 입력데이터의 유효성검사
        /// </summary>
        /// <returns></returns>
        private bool CheckValidation()
        {
            if ( string.IsNullOrEmpty(TxtAuthor.Text) || string.IsNullOrEmpty(TxtNames.Text)
                || DtpReleaseDate.Value == null || string.IsNullOrEmpty(TxtISBN.Text) 
                || string.IsNullOrEmpty(TxtPrice.Text) || CboDivision.SelectedIndex == -1)
            {
                MetroMessageBox.Show(this, "빈값은 처리할 수 없습니다.", "경고",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        /// <summary>
        /// 데이터가 바뀔 때 마다 실행시켜 새로운 데이터를 보여줌
        /// </summary>
        private void RefreshData()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = @"SELECT b.Idx
                                          ,b.Author
                                          ,b.Division
	                                      ,d.Names as DivName
                                          ,b.Names
                                          ,b.ReleaseDate
                                          ,b.ISBN
                                          ,Format(b.Price, '#,##') as 'Price'
                                          ,b.Descriptions
                                      FROM dbo.bookstbl as b
                                     inner join dbo.divtbl as d
	                                    on b.Division = d.Division;";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn); // 가져올 데이터베이스를 변환
                    DataSet ds = new DataSet(); // 가상의 데이터베이스
                    adapter.Fill(ds, "bookstbl");

                    DgvData.DataSource = ds;
                    DgvData.DataMember = "bookstbl";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            var column = DgvData.Columns[2]; //Div column
            column.Visible = false;
            column = DgvData.Columns[8]; //Descriptions column
            column.Visible = false;
            column = DgvData.Columns[4]; // Name column
            column.Width = 250;
            column.HeaderText = "도서명";
            column = DgvData.Columns[0];
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }
        /// <summary>
        /// 입력되어 있는 값들 초기화
        /// Division의 경우 함부러 바꾸지 못 하게 ReadOnly 상태를 걸어주어 이걸 풀어줌
        /// </summary>
        private void ClearInputs()
        {
            TxtIdx.Text = TxtAuthor.Text = TxtNames.Text = 
            TxtISBN.Text = TxtPrice.Text = TxtDescriptions.Text = "";
            CboDivision.SelectedIndex = -1;
            DtpReleaseDate.Value = DateTime.Now;
            // TxtIdx.ReadOnly = false;
            IsNew = true;
        }
        /// <summary>
        /// 데이터 입력 및 수정
        /// Insert 및 Update Query문
        /// </summary>
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
                        query = @"INSERT INTO [dbo].[bookstbl]
                                                   ([Author]
                                                   ,[Division]
                                                   ,[Names]
                                                   ,[ReleaseDate]
                                                   ,[ISBN]
                                                   ,[Price]
                                                   ,[Descriptions])
                                             VALUES
                                                   (@Author,
                                                   @Division,
                                                   @Names,
                                                   @ReleaseDate,
                                                   @ISBN,
                                                   @Price,
                                                   @Descriptions)";
                    }
                    else
                    {
                        query = @"UPDATE [dbo].[bookstbl]
                                           SET [Author] = @Author
                                              ,[Division] = @Division
                                              ,[Names] = @Names
                                              ,[ReleaseDate] = @ReleaseDate
                                              ,[ISBN] = @ISBN
                                              ,[Price] = @Price
                                              ,[Descriptions] = @Descriptions
                                         WHERE Idx = @Idx";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    var pAuthor = new SqlParameter("@Author", SqlDbType.NVarChar, 50);
                    pAuthor.Value = TxtAuthor.Text;
                    cmd.Parameters.Add(pAuthor);

                    var pDivision = new SqlParameter("@Division", SqlDbType.VarChar, 8);
                    pDivision.Value = CboDivision.SelectedValue;
                    cmd.Parameters.Add(pDivision);

                    var pNames = new SqlParameter("@Names", SqlDbType.NVarChar, 100);
                    pNames.Value = TxtNames.Text;
                    cmd.Parameters.Add(pNames);

                    var pReleaseDate = new SqlParameter("@ReleaseDate", SqlDbType.Date);
                    pReleaseDate.Value = DtpReleaseDate.Value;
                    cmd.Parameters.Add(pReleaseDate);

                    var pISBN = new SqlParameter(@"ISBN", SqlDbType.VarChar, 200);
                    pISBN.Value = TxtISBN.Text;
                    cmd.Parameters.Add(pISBN);

                    var pPrice = new SqlParameter(@"Price", SqlDbType.Decimal);
                    pPrice.Value = TxtPrice.Text;
                    cmd.Parameters.Add(pPrice);

                    var pDescriptions = new SqlParameter(@"Descriptions", SqlDbType.NVarChar);
                    pDescriptions.Value = Helper.Common.ReplaceCmdText(TxtDescriptions.Text);
                    cmd.Parameters.Add(pDescriptions);

                    if(IsNew == false) //update일 때만 처리
                    {
                        var pIdx = new SqlParameter(@"Idx", SqlDbType.Int);
                        pIdx.Value = TxtIdx.Text;
                        cmd.Parameters.Add(pIdx);
                    }
                    
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
        /// <summary>
        /// 데이터 삭제
        /// Delete Query문
        /// </summary>
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

                        var query = @"Delete From [dbo].bookstbl
                                            WHERE [Idx] = @Idx";

                        SqlCommand cmd = new SqlCommand(query, conn);

                        var pIdx = new SqlParameter("@Idx", SqlDbType.NVarChar, 45);
                        pIdx.Value = TxtIdx.Text;
                        cmd.Parameters.Add(pIdx);

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
        #endregion
    }
}
