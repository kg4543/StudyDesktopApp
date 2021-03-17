using MetroFramework.Forms;
using MetroFramework;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookRentalShopApp
{
    public partial class FrmMember : MetroForm
    {
        #region 전역변수 영역
        private bool IsNew { get; set; } // false (수정) / true (신규)
        #endregion

        #region 이벤트 영역
        public FrmMember()
        {
            InitializeComponent();
        }

        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            IsNew = true;
            RefreshData();
            ClearInputs();
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
        /// 입력데이터의 유효성검사
        /// </summary>
        /// <returns></returns>
        private bool CheckValidation()
        {
            if ( string.IsNullOrEmpty(TxtName.Text) || string.IsNullOrEmpty(TxtAddr.Text)
                || string.IsNullOrEmpty(TxtMobile.Text) || string.IsNullOrEmpty(TxtEmail.Text) 
                || string.IsNullOrEmpty(TxtUserID.Text) || CboLevels.SelectedIndex == -1)
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

                    var query = @"SELECT   [Idx]
                                          ,[Names]
                                          ,[Levels]
                                          ,[Addr]
                                          ,[Mobile]
                                          ,[Email]
                                          ,[userID]
                                          ,[lastLoginDt]
                                          ,[loginIpAddr]
                                      FROM [dbo].[membertbl]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "membertbl");

                    DgvData.DataSource = ds;
                    DgvData.DataMember = "membertbl";
                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, $"예외발생 : {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 입력되어 있는 값들 초기화
        /// Division의 경우 함부러 바꾸지 못 하게 ReadOnly 상태를 걸어주어 이걸 풀어줌
        /// </summary>
        private void ClearInputs()
        {
            TxtIdx.Text =
            TxtName.Text =
            TxtAddr.Text =
            TxtMobile.Text =
            TxtEmail.Text =
            TxtUserID.Text =
            TxtPasswords.Text = "";
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
                        query = @"INSERT INTO [dbo].[membertbl]
                                                   ([Names]
                                                   ,[Levels]
                                                   ,[Addr]
                                                   ,[Mobile]
                                                   ,[Email]
                                                   ,[userID]
                                                   ,[passwords]
                                                   ,[lastLoginDt]
                                                   ,[loginIpAddr])
                                             VALUES
                                                   (@Names
		                                           ,@Levels
		                                           ,@Addr
		                                           ,@Mobile
		                                           ,@Email
		                                           ,@userID
		                                           ,@passwords";
                    }
                    else
                    {
                        query = @"UPDATE [dbo].[membertbl]
                                     SET [Names] = @Names
                                        ,[Levels] = @Levels
                                        ,[Addr] = @Addr
                                        ,[Mobile] = @Mobile
                                        ,[Email] = @Email
                                        ,[userID] = @userID
                                        ,[passwords] = @passwords
                                   WHERE Idx = @Idx";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    var pNames = new SqlParameter("@Names", SqlDbType.NVarChar, 50);
                    pNames.Value = TxtName.Text;
                    cmd.Parameters.Add(pNames);

                    var pLevels = new SqlParameter("@Levels", SqlDbType.Char, 1);
                    pLevels.Value = CboLevels.SelectedItem.ToString();
                    cmd.Parameters.Add(pLevels);

                    var pAddr = new SqlParameter("@Addr", SqlDbType.NVarChar, 100);
                    pAddr.Value = TxtAddr.Text;
                    cmd.Parameters.Add(pAddr);

                    var pMobile = new SqlParameter(@"Mobile", SqlDbType.VarChar, 13);
                    pMobile.Value = TxtMobile.Text;
                    cmd.Parameters.Add(pMobile);

                    var pEmail = new SqlParameter(@"Email", SqlDbType.VarChar, 50);
                    pEmail.Value = TxtEmail.Text;
                    cmd.Parameters.Add(pEmail);

                    var pUserID = new SqlParameter(@"UserID", SqlDbType.VarChar, 20);
                    pUserID.Value = TxtUserID.Text;
                    cmd.Parameters.Add(pUserID);

                    var pPasswords = new SqlParameter(@"Passwords", SqlDbType.VarChar, 100);
                    pPasswords.Value = TxtPasswords.Text;
                    cmd.Parameters.Add(pPasswords);

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
                if (MetroMessageBox.Show(this, "삭제하시겠습니까?,", "삭제",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                    {
                        if (conn.State == ConnectionState.Closed) conn.Open();

                        var query = "";

                        query = "Delete From [dbo].membertbl" +
                                " WHERE [Idx] = @Idx";

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
