using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WordMemoryWeb
{
    public partial class Quiz : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }
            if (!IsPostBack)
            {
                LoadQuestion();
                btnNext.Visible = false;
            }
        }

        private void LoadQuestion()
        {

            lblMessage.Text = "";

            string connectionString = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;
            int userID = Convert.ToInt32(Session["UserID"]);
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                        SELECT TOP 1 W.WordID, W.EngWordName, W.TurWordName
                        FROM UserWordProgress UWP
                        INNER JOIN Words W ON UWP.WordID = W.WordID
                        WHERE UWP.UserID = @UserID
                        AND UWP.IsLearned = 0
                        AND UWP.NextQuizDate <= GETDATE()
                        ORDER BY NEWID()";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        int wordId = Convert.ToInt32(dr["WordID"]);
                        string engWord = dr["EngWordName"].ToString();
                        string correctAnswer = dr["TurWordName"].ToString();
                        hfWordID.Value = wordId.ToString();
                        hfCorrectAnswer.Value = correctAnswer;
                        lblQuestion.Text = engWord;
                        dr.Close();
                        LoadOptions(conn, correctAnswer);
                    }
                    else
                    {
                        dr.Close();

                        lblQuestion.Text = "Bugün için sorulacak kelime yok.";
                        btnOption1.Visible = false;
                        btnOption2.Visible = false;
                        btnOption3.Visible = false;
                        btnOption4.Visible = false;
                    }
                }
            }
        }

        private void LoadOptions(SqlConnection conn, string correctAnswer)
        {
            List<string> options = new List<string>();
            options.Add(correctAnswer);

            string query = @"
                    SELECT TOP 3 TurWordName
                    FROM Words
                    WHERE TurWordName <> @CorrectAnswer
                    ORDER BY NEWID()";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@CorrectAnswer", correctAnswer);

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    options.Add(dr["TurWordName"].ToString());
                }

                dr.Close();
            }

            Random rnd = new Random();
            options = options.OrderBy(x => rnd.Next()).ToList();

            btnOption1.Text = options[0].Trim();
            btnOption2.Text = options[1].Trim();
            btnOption3.Text = options[2].Trim();
            btnOption4.Text = options[3].Trim();

            btnOption1.Visible = true;
            btnOption2.Visible = true;
            btnOption3.Visible = true;
            btnOption4.Visible = true;
        }

        protected void Answer_Click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;

            string selectedAnswer = clickedButton.Text.Trim();
            string correctAnswer = hfCorrectAnswer.Value.Trim();

            int userID = Convert.ToInt32(Session["UserID"]);
            int wordID = Convert.ToInt32(hfWordID.Value);

            bool isCorrect = selectedAnswer.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase);

            SaveQuizResult(userID, wordID, isCorrect);
            UpdateProgress(userID, wordID, isCorrect);

            if (isCorrect)
            {

                lblMessage.ForeColor = System.Drawing.Color.Green;
                clickedButton.CssClass = "btn btn-success";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                clickedButton.CssClass = "btn btn-danger";
                HighlightCorrectAnswer(correctAnswer);
            }
            btnOption1.OnClientClick = "return false;";
            btnOption2.OnClientClick = "return false;";
            btnOption3.OnClientClick = "return false;";
            btnOption4.OnClientClick = "return false;";
            btnNext.Visible = true;
        }

        private void HighlightCorrectAnswer(string correctAnswer)
        {
            if (btnOption1.Text.Trim() == correctAnswer)
                btnOption1.CssClass = "btn btn-success";

            if (btnOption2.Text.Trim() == correctAnswer)
                btnOption2.CssClass = "btn btn-success";

            if (btnOption3.Text.Trim() == correctAnswer)
                btnOption3.CssClass = "btn btn-success";

            if (btnOption4.Text.Trim() == correctAnswer)
                btnOption4.CssClass = "btn btn-success";
        }
        private void SaveQuizResult(int userID, int wordID, bool isCorrect)
        {
            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
            INSERT INTO QuizResults (UserID, WordID, IsCorrect)
            VALUES (@UserID, @WordID, @IsCorrect)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    cmd.Parameters.AddWithValue("@WordID", wordID);
                    cmd.Parameters.AddWithValue("@IsCorrect", isCorrect);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void UpdateProgress(int userID, int wordID, bool isCorrect)
        {
            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                int currentStep = 0;

                string getQuery = @"
                    SELECT CurrentStep 
                    FROM UserWordProgress 
                    WHERE UserID = @UserID AND WordID = @WordID";

                using (SqlCommand getCmd = new SqlCommand(getQuery, conn))
                {
                    getCmd.Parameters.AddWithValue("@UserID", userID);
                    getCmd.Parameters.AddWithValue("@WordID", wordID);

                    object result = getCmd.ExecuteScalar();

                    if (result != null)
                    {
                        currentStep = Convert.ToInt32(result);
                    }
                }

                if (isCorrect)
                {
                    currentStep++;

                    DateTime nextQuizDate = DateTime.Now;

                    if (currentStep == 1)
                        nextQuizDate = DateTime.Now.AddDays(1);
                    else if (currentStep == 2)
                        nextQuizDate = DateTime.Now.AddDays(7);
                    else if (currentStep == 3)
                        nextQuizDate = DateTime.Now.AddMonths(1);
                    else if (currentStep == 4)
                        nextQuizDate = DateTime.Now.AddMonths(3);
                    else if (currentStep == 5)
                        nextQuizDate = DateTime.Now.AddMonths(6);
                    else if (currentStep >= 6)
                        nextQuizDate = DateTime.Now.AddYears(1);

                    bool isLearned = currentStep >= 6;

                    string updateQuery = @"
                         UPDATE UserWordProgress
                         SET 
                         CorrectStreak = @CurrentStep,
                         CurrentStep = @CurrentStep,
                         NextQuizDate = @NextQuizDate,
                         IsLearned = @IsLearned,
                         LastAnswerDate = GETDATE()
                         WHERE UserID = @UserID AND WordID = @WordID";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@CurrentStep", currentStep);
                        updateCmd.Parameters.AddWithValue("@NextQuizDate", nextQuizDate);
                        updateCmd.Parameters.AddWithValue("@IsLearned", isLearned);
                        updateCmd.Parameters.AddWithValue("@UserID", userID);
                        updateCmd.Parameters.AddWithValue("@WordID", wordID);

                        updateCmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string updateQuery = @"
                        UPDATE UserWordProgress
                        SET 
                        CorrectStreak = 0,
                        CurrentStep = 0,
                        NextQuizDate = GETDATE(),
                        IsLearned = 0,
                        LastAnswerDate = GETDATE()
                        WHERE UserID = @UserID AND WordID = @WordID";

                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@UserID", userID);
                        updateCmd.Parameters.AddWithValue("@WordID", wordID);

                        updateCmd.ExecuteNonQuery();
                    }
                }
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            btnOption1.OnClientClick = "";
            btnOption2.OnClientClick = "";
            btnOption3.OnClientClick = "";
            btnOption4.OnClientClick = "";

            btnOption1.CssClass = "btn btn-outline-primary";
            btnOption2.CssClass = "btn btn-outline-primary";
            btnOption3.CssClass = "btn btn-outline-primary";
            btnOption4.CssClass = "btn btn-outline-primary";

            LoadQuestion();
        }
    }
}