using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WordMemoryWeb
{
    public partial class WordDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] == null)
                {
                Response.Redirect("WordList.aspx");
                }

                int wordId = Convert.ToInt32(Request.QueryString["id"]);

                LoadWordDetail(wordId);
                LoadSamples(wordId);


            }
        }

        private void LoadWordDetail(int wordID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            SELECT EngWordName, TurWordName, PicturePath, Category
            FROM Words
            WHERE WordID = @WordID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@WordID", wordID);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        lblEngWord.Text = dr["EngWordName"].ToString();
                        lblTurWord.Text = dr["TurWordName"].ToString();
                        lblCategory.Text = dr["Category"].ToString();

                        string picturePath = dr["PicturePath"].ToString();

                        if (!string.IsNullOrEmpty(picturePath))
                        {
                            imgWord.ImageUrl = picturePath;
                        }
                        else
                        {
                            imgWord.ImageUrl = "https://via.placeholder.com/250";
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Kelime bulunamadı.";
                    }

                    dr.Close();
                }
            }
        }

        private void LoadSamples(int wordID)
        {
            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = @"
            SELECT SampleSentence
            FROM WordSamples
            WHERE WordID = @WordID";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@WordID", wordID);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        rptSamples.DataSource = dt;
                        rptSamples.DataBind();
                    }
                }
            }
        }

    }
}