<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="WordMemoryWeb.Settings" %>

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

        <h3 class="text-center mb-4">Ayarlar</h3>

        <asp:Label ID="lblMessage" runat="server"></asp:Label>

        <div class="mb-3">
            <label class="form-label">Günlük Yeni Kelime Sayısı</label>

            <asp:DropDownList ID="ddlNewWordCount" runat="server" CssClass="form-select">
                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                <asp:ListItem Text="20" Value="20"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Kaydet"
            CssClass="btn btn-primary w-100" OnClick="btnSave_Click" />

        <div class="text-center mt-3">
            <a href="Dashboard.aspx">Panele Dön</a>
        </div>

    </div>
</div>
    </form>
</body>
</html>
