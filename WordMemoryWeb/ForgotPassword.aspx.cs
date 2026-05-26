using System;
using System.Configuration;
using System.Data.SqlClient;

namespace WordMemoryWeb
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (email == "" || newPassword == "" || confirmPassword == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Lütfen tüm alanları doldurun.";
                return;
            }

            if (newPassword != confirmPassword)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Şifreler eşleşmiyor.";
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

                    if (count == 0)
                    {
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Bu email adresiyle kayıtlı kullanıcı bulunamadı.";
                        return;
                    }
                }

                string updateQuery = @"
            UPDATE Users
            SET Password = @Password
            WHERE Email = @Email";

                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("@Password", newPassword);
                    updateCmd.Parameters.AddWithValue("@Email", email);

                    updateCmd.ExecuteNonQuery();
                }
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Şifreniz başarıyla güncellendi. Giriş sayfasına dönebilirsiniz.";
        }
    }
}