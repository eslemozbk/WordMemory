<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="WordMemoryWeb.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">

<body>
    <form id="form1" runat="server">

        <div class="container d-flex justify-content-center align-items-center min-vh-100">
            <div class="card shadow p-4" style="width: 400px;">
                <h3 class="text-center mb-4">Kelime Ezberleme Sistemi</h3>

                <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>

                <div class="mb-3">
                    <label class="form-label">Kullanıcı Adı</label>
                    <asp:TextBox ID="txtUserName" runat="server" CssClass="form-control" placeholder="Kullanıcı adınızı girin"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email girin"></asp:TextBox>
                </div>

                <div class="mb-3">
                    <label class="form-label">Şifre</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Şifre girin"></asp:TextBox>
                </div>

                <asp:Button ID="btnRegister" runat="server" Text="Kayıt Ol" CssClass="btn btn-primary w-100" OnClick="btnRegister_Click" />

                <div class="text-center mt-3">
                    <a href="Login.aspx">Zaten hesabın var mı? Giriş yap</a>
                </div>
            </div>
        </div>

    </form>
</body>
</html>
