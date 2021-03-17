using MetroFramework;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookRentalShopApp
{
    public partial class FrmLogin : MetroForm
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            var strUserId = "";

            // MessageBox.Show("로그인");
            if (string.IsNullOrEmpty(TxtUserID.Text) || string.IsNullOrEmpty(TxtPassword.Text))
            {
                MetroMessageBox.Show(this,"아이디/패스워드를 입력하세요!","오류",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Helper.Common.ConnString))
                {
                    if (conn.State == ConnectionState.Closed) conn.Open();

                    var query = "select userID from membertbl " +
                                " where userID = @userID " +
                                " and passwords = @passwords ";

                    // Sql Command 생성
                    SqlCommand cmd = new SqlCommand(query, conn);
                    // Sql injection 예방
                    SqlParameter pUserID = new SqlParameter("@userID", SqlDbType.VarChar, 20);
                    pUserID.Value = TxtUserID.Text;
                    cmd.Parameters.Add(pUserID);

                    SqlParameter pPassword = new SqlParameter("@Passwords", SqlDbType.VarChar, 20);
                    pPassword.Value = TxtPassword.Text;
                    cmd.Parameters.Add(pPassword);

                    // SqlDataReader 생성
                    SqlDataReader reader = cmd.ExecuteReader();

                    //reader로 처리...
                    reader.Read();
                    strUserId = reader["UserID"] != null ? reader["userID"].ToString() : "";
                    reader.Close();

                    if (string.IsNullOrEmpty(strUserId))
                    {
                        MetroMessageBox.Show(this, "접속실패", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }
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

                }
            }
            catch (Exception ex)
            {
                MetroMessageBox.Show(this, "접속실패", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MetroMessageBox.Show(this, $"Error : {ex.Message}", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            Environment.Exit(0); //종료
        }

        private void TxtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) TxtPassword.Focus();
            
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) BtnLogin.Focus();
        }

    }
}
