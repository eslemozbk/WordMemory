<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="WordMemoryWeb.ForgotPassword" %>

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
    <div class="card shadow p-4 mx-auto" style="max-width: 500px;">

        <h3 class="text-center mb-4">Şifremi Unuttum</h3>

        <asp:Label ID="lblMessage" runat="server"></asp:Label>

        <div class="mb-3">
            <label class="form-label">Email</label>
            <asp:TextBox ID="txtEmail" runat="server"
                CssClass="form-control"
                placeholder="Kayıtlı email adresinizi girin">
            </asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">Yeni Şifre</label>
            <asp:TextBox ID="txtNewPassword" runat="server"
                TextMode="Password"
                CssClass="form-control"
                placeholder="Yeni şifrenizi girin">
            </asp:TextBox>
        </div>

        <div class="mb-3">
            <label class="form-label">Yeni Şifre Tekrar</label>
            <asp:TextBox ID="txtConfirmPassword" runat="server"
                TextMode="Password"
                CssClass="form-control"
                placeholder="Yeni şifreyi tekrar girin">
            </asp:TextBox>
        </div>

        <asp:Button ID="btnResetPassword" runat="server"
            Text="Şifreyi Güncelle"
            CssClass="btn btn-primary w-100"
            OnClick="btnResetPassword_Click" />

        <div class="text-center mt-3">
            <a href="Login.aspx">Giriş sayfasına dön</a>
        </div>

    </div>
</div>
    </form>
</body>
</html>
