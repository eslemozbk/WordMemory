<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordAdd.aspx.cs" Inherits="WordMemoryWeb.WordAdd" %>

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
        <h3 class="mb-4 text-center">Kelime Ekle</h3>

        <asp:Label ID="lblMessage" runat="server"></asp:Label>

        <div class="mb-3">
            <label class="form-label">İngilizce Kelime</label>
            <asp:TextBox ID="txtEngWord" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">Türkçe Karşılığı</label>
            <asp:TextBox ID="txtTurWord" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">Kategori</label>
            <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">Örnek Cümle 1</label>
            <asp:TextBox ID="txtSample1" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">Örnek Cümle 2</label>
            <asp:TextBox ID="txtSample2" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">Resim</label>
            <asp:FileUpload ID="fuPicture" runat="server" CssClass="form-control" />
        </div>

        <asp:Button ID="btnSave" runat="server" Text="Kaydet" CssClass="btn btn-primary w-100" OnClick="btnSave_Click" />

        <div class="mt-3 text-center">
            <a href="Dashboard.aspx">Panele Dön</a>
        </div>
    </div>
</div>
    </form>
</body>
</html>
