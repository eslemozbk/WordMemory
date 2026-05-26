using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WordMemoryWeb
{
    public partial class Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {                
                Response.Redirect("Login.aspx");
            }
            
            if (!IsPostBack)
            {
                LoadSummrayReport();
                LoadCategoryReport();
            }
        }

        private void LoadSummrayReport()
        {
            int userId = Convert.ToInt32(Session["UserID"]);
            string connectionString = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                        SELECT 
                            COUNT(*) AS TotalAnswers,

                            SUM(CASE 
                                WHEN IsCorrect = 1 THEN 1 
                                ELSE 0 
                            END) AS CorrectAnswers,

                            SUM(CASE 
                                WHEN IsCorrect = 0 THEN 1 
                                ELSE 0 
                            END) AS WrongAnswers,

                            CAST(
                                ISNULL(
                                    SUM(CASE WHEN IsCorrect = 1 THEN 1 ELSE 0 END) * 100.0 
                                    / NULLIF(COUNT(*), 0),
                                0)
                            AS DECIMAL(5,2)) AS SuccessRate

                        FROM QuizResults
                        WHERE UserID = @UserID";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userId);
                    SqlDataReader dr = cmd.ExecuteReader();
                    
                        if (dr.Read())
                        {
                            lblTotal.Text = dr["TotalAnswers"].ToString();
                            lblCorrect.Text = dr["CorrectAnswers"].ToString();
                            lblWrong.Text = dr["WrongAnswers"].ToString();
                            lblSuccess.Text = dr["SuccessRate"].ToString() + "%";
                        }

                      dr.Close();

                }
            }
        }

        private void LoadCategoryReport()
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
                    SELECT 
                        ISNULL(W.Category, 'Kategori Yok') AS Category,
                        COUNT(*) AS TotalAnswers,
                        SUM(CASE WHEN QR.IsCorrect = 1 THEN 1 ELSE 0 END) AS CorrectAnswers,
                        CAST(
                            SUM(CASE WHEN QR.IsCorrect = 1 THEN 1 ELSE 0 END) * 100.0 / COUNT(*)
                        AS DECIMAL(5,2)) AS SuccessRate
                    FROM QuizResults QR
                    INNER JOIN Words W ON QR.WordID = W.WordID
                    WHERE QR.UserID = @UserID
                    GROUP BY W.Category";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        gvCategoryReport.DataSource = dt;
                        gvCategoryReport.DataBind();
                    }
                }
            }
        }

    }
}