using MetroFramework.Forms;
using MetroFramework;
using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace BookRentalShopApp
{
    public partial class FrmBooksPopup : MetroForm
    {
        #region 전역변수
        public int SelIdx { get; set; }

        public string SelName { get; set; }

        #endregion

        #region 이벤트 영역
        public FrmBooksPopup()
        {
            InitializeComponent();
        }

        private void FrmDivCode_Load(object sender, EventArgs e)
        {
            RefreshData(); //테이블 조회
        }

        private void FrmDivCode_Resize(object sender, EventArgs e)
        {
        }
       
        #endregion

        #region 커스템 메소드 영역

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
            
            column = DgvData.Columns[4]; // Name column
            column.Width = 250;
            column.HeaderText = "도서명";
            column = DgvData.Columns[0];
            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            DgvData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }
        #endregion
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
                SelName = DgvData.SelectedRows[0].Cells[4].Value.ToString();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
