<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="WordMemoryWeb.Dashboard" %>

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
        <h2 class="text-center mb-3">Kelime Ezberleme Sistemi</h2>

        <p class="text-center">
            <asp:Label ID="lblWelcome" runat="server" CssClass="fw-bold"></asp:Label>
        </p>

        <div class="row text-center mt-4">
            <div class="col-md-3 mb-3">
                <a href="WordAdd.aspx" class="btn btn-outline-primary w-100 p-3">Kelime Ekle</a>
            </div>

            <div class="col-md-3 mb-3">
                <a href="Quiz.aspx" class="btn btn-outline-primary w-100 p-3">Sınava Başla</a>
            </div>

            <div class="col-md-3 mb-3">
                <a href="Settings.aspx" class="btn btn-outline-primary w-100 p-3">Ayarlar</a>
            </div>

            <div class="col-md-3 mb-3">
                <a href="Report.aspx" class="btn btn-outline-primary w-100 p-3">Analiz Raporu</a>
            </div>

            <div class="col-md-3 mb-3">
                <a href="WordList.aspx" class="btn btn-outline-primary w-100 p-3">Kelime Listesi</a>
            </div>

            <div class="col-md-3 mb-3">
                <a href="Wordle.aspx" class="btn btn-outline-primary w-100 p-3">Wordle</a>
    
</div>
             </div>
        <div class="d-flex justify-content-center mt-4">
    <a href="Login.aspx" class="btn btn-secondary px-4">
        Çıkış Yap
    </a>
</div>
        </form>
</body>
</html>
