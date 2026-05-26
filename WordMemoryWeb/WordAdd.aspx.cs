using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WordMemoryWeb
{
    public partial class WordAdd : System.Web.UI.Page
    {
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string engWord = txtEngWord.Text.Trim();
            string turWord = txtTurWord.Text.Trim();
            string category = txtCategory.Text.Trim();
            string sample1 = txtSample1.Text.Trim();
            string sample2 = txtSample2.Text.Trim();

            if (engWord == "" || turWord == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "İngilizce kelime ve Türkçe karşılık zorunludur.";
                return;
            }

            string picturePath = null;

            if (fuPicture.HasFile)
            {
                string fileName = Path.GetFileName(fuPicture.FileName);
                string savePath = Server.MapPath("~/Uploads/Words/" + fileName);
                fuPicture.SaveAs(savePath);

                picturePath = "/Uploads/Words/" + fileName;
            }

            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string insertWordQuery = @"
            INSERT INTO Words (EngWordName, TurWordName, PicturePath, Category)
            OUTPUT INSERTED.WordID
            VALUES (@EngWordName, @TurWordName, @PicturePath, @Category)";

                int wordID;

                using (SqlCommand cmd = new SqlCommand(insertWordQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@EngWordName", engWord);
                    cmd.Parameters.AddWithValue("@TurWordName", turWord);
                    cmd.Parameters.AddWithValue("@PicturePath", (object)picturePath ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Category", category);

                    wordID = (int)cmd.ExecuteScalar();

                    string progressQuery = @" INSERT INTO UserWordProgress (UserID, WordID, CorrectStreak, CurrentStep, NextQuizDate, Islearned)
                                        SELECT UserID, @WordID, 0, 0, GETDATE(), 0
                                        FROM Users";

                    using (SqlCommand progressCmd = new SqlCommand(progressQuery, conn)) {  
                        progressCmd.Parameters.AddWithValue("@WordID", wordID);
                        progressCmd.ExecuteNonQuery();
                    }
                }

                if (sample1 != "")
                {
                    InsertSample(conn, wordID, sample1);
                }

                if (sample2 != "")
                {
                    InsertSample(conn, wordID, sample2);
                }
            }

            lblMessage.ForeColor = System.Drawing.Color.Green;
            lblMessage.Text = "Kelime başarıyla eklendi.";

            txtEngWord.Text = "";
            txtTurWord.Text = "";
            txtCategory.Text = "";
            txtSample1.Text = "";
            txtSample2.Text = "";
        }

        private void InsertSample(SqlConnection conn, int wordID, string sampleText)
        {
            string insertSampleQuery = @"
            INSERT INTO Samples (WordID, SampleSentence)
            VALUES (@WordID, @SampleSentence)";
            using (SqlCommand cmd = new SqlCommand(insertSampleQuery, conn))
            {
                cmd.Parameters.AddWithValue("@WordID", wordID);
                cmd.Parameters.AddWithValue("@SampleSentence", sampleText);
                cmd.ExecuteNonQuery();
            }
        }

    }

}