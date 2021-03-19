using MetroFramework.Forms;
using MetroFramework;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Diagnostics;

namespace BookRentalShopApp
{
    public partial class FrmRental : MetroForm
    {
        #region 전역변수 영역
        private bool IsNew { get; set; } // false (수정) / true (신규)
        private int selMemberIdx = 0; //선택된 회원번호
        private string selMemberName = ""; //선택된 회원이름
        private int selBookIdx = 0; //선택된 책번호
        private string selBookName = ""; //선택된 책이름
        #endregion

        #region 이벤트 영역
        public FrmRental()
        {
            InitializeComponent();
        }

        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            IsNew = true; //신규인지 구분(초기화)
            InitCboData(); // 콤보박스 들어가는 데이터 조회
            RefreshData(); //테이블 조회
            ClearInputs();

            DtpRentalDate.CustomFormat = "yyyy-MM-dd";
            DtpRentalDate.Format = DateTimePickerFormat.Custom;
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
                BtnSearchMember.Enabled = BtnSearchBook.Enabled = false;
                DtpRentalDate.Enabled = false;
            }
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (CheckValidation() == false) return;

            SaveData();
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

        /// <summary>
        /// 데이터 값 선택 시 load
        /// </summary>
        /// <param name="selData"></param>
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

        /// <summary>
        /// 입력데이터의 유효성검사
        /// </summary>
        /// <returns></returns>
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
            /*column = DgvData.Columns[7]; //rentalState column
            column.Visible = false;*/

        }
        /// <summary>
        /// 입력되어 있는 값들 초기화
        /// Division의 경우 함부러 바꾸지 못 하게 ReadOnly 상태를 걸어주어 이걸 풀어줌
        /// </summary>
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
        /// <summary>
        /// 데이터 입력 및 수정
        /// Insert 및 Update Query문
        /// </summary>
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

                    if (IsNew == true)
                    {
                        var pMemberIdx = new SqlParameter("@memberIdx", SqlDbType.Int);
                        pMemberIdx.Value = selMemberIdx;
                        cmd.Parameters.Add(pMemberIdx);

                        var pBookIdx = new SqlParameter("@bookIdx", SqlDbType.Int);
                        pBookIdx.Value = selBookIdx;
                        cmd.Parameters.Add(pBookIdx);

                        var pRentalDate = new SqlParameter("@rentalDate", SqlDbType.Date);
                        pRentalDate.Value = DtpRentalDate.Value;
                        cmd.Parameters.Add(pRentalDate);

                        var pRentalState = new SqlParameter("@rentalState", SqlDbType.Char, 1);
                        pRentalState.Value = CboRentalState.SelectedValue;
                        cmd.Parameters.Add(pRentalState);
                    }
                    else //update일 때만 처리
                    {
                        var pRentalState = new SqlParameter("@rentalState", SqlDbType.Char, 1);
                        pRentalState.Value = CboRentalState.SelectedValue;
                        cmd.Parameters.Add(pRentalState);

                        var pIdx = new SqlParameter("@Idx", SqlDbType.Int);
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
      
        private void BtnSearchMember_Click(object sender, EventArgs e)
        {
            FrmMemberPopup frm = new FrmMemberPopup();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                selMemberIdx = frm.SelIdx;
                TxtMemberName.Text = selMemberName = frm.SelName;
            }
        }

        private void BtnSearchBook_Click(object sender, EventArgs e)
        {
            FrmBooksPopup frm = new FrmBooksPopup();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                selBookIdx = frm.SelIdx;
                TxtBookName.Text = selBookName = frm.SelName;
            }
        }
        #endregion


    }
}
