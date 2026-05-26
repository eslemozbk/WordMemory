using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace WordMemoryWeb
{
    public partial class Wordle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] == null)
            {
                Response.Redirect("Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadRandomLearnedWord();
                Session["Guesses"] = new List<string>();
                BuildBoard();
            }
        }

        private void LoadRandomLearnedWord()
        {
            int userID = Convert.ToInt32(Session["UserID"]);
            string connStr = ConfigurationManager.ConnectionStrings["WordMemoryDB"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();

                string query = @"
                    SELECT TOP 1 W.EngWordName
                    FROM UserWordProgress UWP
                    INNER JOIN Words W ON UWP.WordID = W.WordID
                    WHERE UWP.UserID = @UserID
                    AND UWP.IsLearned = 1
                    AND LEN(W.EngWordName) = 5
                    ORDER BY NEWID()";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        hfTargetWord.Value = result.ToString().ToUpper();
                        lblMessage.ForeColor = System.Drawing.Color.Black;
                        lblMessage.Text = "Öğrenilen kelimelerden bir bulmaca oluşturuldu.";
                    }
                    else
                    {
                        hfTargetWord.Value = "";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                        lblMessage.Text = "Wordle için önce en az bir kelime öğrenmelisiniz.";
                        btnGuess.Enabled = false;
                    }
                }
            }
        }

        protected void btnGuess_Click(object sender, EventArgs e)
        {
            string target = hfTargetWord.Value.Trim().ToUpper();
            string guess = hfGuess.Value.Trim().ToUpper();

            if (target == "")
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Bulmaca için öğrenilmiş kelime bulunamadı.";
                return;
            }

            if (guess.Length != target.Length)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Tahmin " + target.Length + " harfli olmalıdır.";
                return;
            }

            List<string> guesses = Session["Guesses"] as List<string>;

            guesses.Add(guess);
            Session["Guesses"] = guesses;

            BuildBoard();

            if (guess == target)
            {
                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Tebrikler! Kelimeyi doğru bildiniz.";
                btnGuess.Enabled = false;
            }
            else if (guesses.Count >= 6)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Hakkınız bitti. Doğru kelime: " + target;
                btnGuess.Enabled = false;
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Black;
                lblMessage.Text = "Tekrar deneyin.";
            }

            hfGuess.Value = "";
        }


        private void BuildBoard()
        {
            string target = hfTargetWord.Value.Trim().ToUpper();
            List<string> guesses = Session["Guesses"] as List<string>;

            int wordLength = 5;

            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < 6; row++)
            {
                if (guesses != null && row == guesses.Count)
                    sb.Append("<div class='word-row active-row'>");
                else
                    sb.Append("<div class='word-row'>");

                if (guesses != null && row < guesses.Count)
                {
                    string guess = guesses[row];

                    for (int i = 0; i < wordLength; i++)
                    {
                        string css = "wrong";

                        if (i < target.Length && i < guess.Length && guess[i] == target[i])
                            css = "correct";
                        else if (i < guess.Length && target.Contains(guess[i].ToString()))
                            css = "exist";

                        sb.Append("<div class='word-cell " + css + "'>");
                        sb.Append(i < guess.Length ? guess[i].ToString() : "");
                        sb.Append("</div>");
                    }
                }
                else
                {
                    for (int i = 0; i < wordLength; i++)
                    {
                        sb.Append("<div class='word-cell empty'></div>");
                    }
                }

                sb.Append("</div>");
            }

            ltBoard.Text = sb.ToString();
        }
    }
}