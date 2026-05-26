using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WordMemoryWeb
{
    public partial class Register : System.Web.UI.Page
    {
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (userName == "" || email == "" || password == "")
            {
                lblMessage.Text = "Lütfen tüm alanları doldurun.";
                return;
            }

            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Email = @Email";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@Email", email);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        lblMessage.Text = "Bu email zaten kayıtlı.";
                        return;
                    }
                }

                string insertQuery = @"
            INSERT INTO Users (UserName, Email, Password)
            OUTPUT INSERTED.UserID
            VALUES (@UserName, @Email, @Password)";

                int newUserID;

                using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@UserName", userName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    newUserID = Convert.ToInt32(cmd.ExecuteScalar());
                }

                string progressQuery = @"
            INSERT INTO UserWordProgress 
            (UserID, WordID, CorrectStreak, CurrentStep, NextQuizDate, IsLearned)
            SELECT @UserID, WordID, 0, 0, GETDATE(), 0
            FROM Words";

                using (SqlCommand progressCmd = new SqlCommand(progressQuery, conn))
                {
                    progressCmd.Parameters.AddWithValue("@UserID", newUserID);
                    progressCmd.ExecuteNonQuery();
                }

                string settingQuery = @"
            INSERT INTO UserSettings (UserID, NewWordCount)
            VALUES (@UserID, 10)";

                using (SqlCommand settingCmd = new SqlCommand(settingQuery, conn))
                {
                    settingCmd.Parameters.AddWithValue("@UserID", newUserID);
                    settingCmd.ExecuteNonQuery();
                }
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Kayıt başarılı. Giriş sayfasına yönlendiriliyorsunuz.";

            Response.Redirect("Login.aspx");
        }


    }
    
}