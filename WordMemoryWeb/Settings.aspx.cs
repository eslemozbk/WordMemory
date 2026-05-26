using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace WordMemoryWeb
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadSettings();
            }
        }

        private void LoadSettings()
        {
           int userID = Convert.ToInt32(Session["UserID"]);
           string connectionString = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

           using (SqlConnection conn = new SqlConnection(connectionString))
           {
                conn.Open();
               string query = "SELECT NewWordCount FROM UserSettings WHERE UserID = @UserID";
               SqlCommand cmd = new SqlCommand(query, conn);
               cmd.Parameters.AddWithValue("@UserID", userID);
                object result = cmd.ExecuteScalar();
               if (result != null)
               {
                   ddlNewWordCount.SelectedValue = result.ToString();
                }
               else
                {
                    ddlNewWordCount.SelectedValue = "10";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            int newWordCount = Convert.ToInt32(ddlNewWordCount.SelectedValue);

            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string checkQuery = "SELECT COUNT(*) FROM UserSettings WHERE UserID = @UserID";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("@UserID", userID);

                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        string updateQuery = @"
                    UPDATE UserSettings
                    SET NewWordCount = @NewWordCount
                    WHERE UserID = @UserID";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@NewWordCount", newWordCount);
                            updateCmd.Parameters.AddWithValue("@UserID", userID);
                            updateCmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string insertQuery = @"
                    INSERT INTO UserSettings (UserID, NewWordCount)
                    VALUES (@UserID, @NewWordCount)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@UserID", userID);
                            insertCmd.Parameters.AddWithValue("@NewWordCount", newWordCount);
                            insertCmd.ExecuteNonQuery();
                        }
                    }
                }
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Ayarlar başarıyla kaydedildi.";
        }
    }
}