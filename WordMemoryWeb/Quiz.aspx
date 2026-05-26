<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="WordMemoryWeb.Quiz" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5">
    <div class="card shadow p-4">

        <h3 class="text-center mb-4">Kelime Sınavı</h3>

        <asp:Label ID="lblMessage" runat="server"></asp:Label>

        <asp:HiddenField ID="hfWordID" runat="server" />
        <asp:HiddenField ID="hfCorrectAnswer" runat="server" />

        <div class="text-center mb-4">
            <h4>Bu kelimenin Türkçe karşılığı nedir?</h4>
            <h2>
                <asp:Label ID="lblQuestion" runat="server"></asp:Label>
            </h2>
        </div>

        <div class="d-grid gap-2">
            <asp:Button ID="btnOption1" runat="server" CssClass="btn btn-outline-dark" OnClick="Answer_Click" />
            <asp:Button ID="btnOption2" runat="server" CssClass="btn btn-outline-dark" OnClick="Answer_Click" />
            <asp:Button ID="btnOption3" runat="server" CssClass="btn btn-outline-dark" OnClick="Answer_Click" />
            <asp:Button ID="btnOption4" runat="server" CssClass="btn btn-outline-dark" OnClick="Answer_Click" />
        </div>

        <hr />

        <div class="text-center">
            <asp:Button ID="btnNext" runat="server" Text="Sonraki Soru" CssClass="btn btn-success" OnClick="btnNext_Click" />
            <a href="Dashboard.aspx" class="btn btn-secondary">Panele Dön</a>
        </div>

    </div>
</div>
    </form>
</body>
</html>
