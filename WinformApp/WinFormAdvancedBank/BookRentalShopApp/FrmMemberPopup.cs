using MetroFramework.Forms;
using MetroFramework;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BookRentalShopApp
{
    public partial class FrmMemberPopup : MetroForm
    {
        #region 전역변수

        public int SelIdx { get; set; }

        public string SelName { get; set; }

        #endregion
        #region 이벤트 영역
        public FrmMemberPopup()
        {
            InitializeComponent();
        }

        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
       
        private void FrmDivCode_Resize(object sender, EventArgs e)
        {
        }
   
        #endregion

        #region 커스템 메소드 영역
        
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
            DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        
    }
}
