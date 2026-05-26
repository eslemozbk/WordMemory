<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Wordle.aspx.cs" Inherits="WordMemoryWeb.Wordle" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

<script>
    document.addEventListener("keydown", function (e) {
        const cells = document.querySelectorAll(".active-row .word-cell");
        const hiddenGuess = document.getElementById("<%= hfGuess.ClientID %>");

    if (!cells || cells.length === 0 || !hiddenGuess) {
        return;
    }

    if (/^[a-zA-Z]$/.test(e.key)) {
        e.preventDefault();

        for (let i = 0; i < cells.length; i++) {
            if (cells[i].innerText === "") {
                cells[i].innerText = e.key.toUpperCase();
                break;
            }
        }
    }

    if (e.key === "Backspace") {
        e.preventDefault();

        for (let i = cells.length - 1; i >= 0; i--) {
            if (cells[i].innerText !== "") {
                cells[i].innerText = "";
                break;
            }
        }
    }

    let guess = "";
    cells.forEach(cell => {
        guess += cell.innerText;
    });

    hiddenGuess.value = guess;

    if (e.key === "Enter") {
        e.preventDefault();

        document.getElementById("<%= btnGuess.ClientID %>").click();
    }
});
</script>
<style>

.word-row {
    display: flex;
    justify-content: center;
    gap: 8px;
    margin-bottom: 8px;
}

.word-cell {
    width: 65px;
    height: 65px;
    border: 2px solid #ccc;
    border-radius: 8px;
    font-size: 32px;
    font-weight: bold;
    display: flex;
    align-items: center;
    justify-content: center;
}

.correct { background: green; color: white; }
.exist { background: goldenrod; color: white; }
.wrong { background: gray; color: white; }
.empty { background: white; color: black; }

.correct{
    background-color:green !important;
    color:white !important;
}

.exist{
    background-color:goldenrod !important;
    color:white !important;
}

.wrong{
    background-color:gray !important;
    color:white !important;
}

</style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container mt-5">
        <div class="card shadow p-4">

            <h3 class="text-center mb-4">Wordle Bulmaca</h3>

            <asp:Label ID="lblMessage" runat="server"></asp:Label>

            <asp:HiddenField ID="hfTargetWord" runat="server" />
            <asp:HiddenField ID="hfGuess" runat="server" />

            <div class="mb-3">
                <label class="form-label">Tahmininizi girin</label>

                <asp:Literal ID="ltBoard" runat="server"></asp:Literal>

            </div>

            <asp:Button ID="btnGuess" runat="server" Text="Tahmin Et"
                CssClass="btn btn-primary w-100" OnClick="btnGuess_Click" />

            <hr />

            <div class="text-center mt-4">
                <a href="Dashboard.aspx" class="btn btn-secondary">Panele Dön</a>
            </div>

        </div>
    </div>
</form>
</body>
</html>
