<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordList.aspx.cs" Inherits="WordMemoryWeb.WordList" %>

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

        <h3 class="text-center mb-4">Kelime Listesi</h3>

        <asp:GridView ID="gvWords"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover"
            GridLines="None">

            <Columns>

                <asp:BoundField DataField="WordID" HeaderText="ID" />

                <asp:BoundField DataField="EngWordName" HeaderText="İngilizce" />

                <asp:BoundField DataField="TurWordName" HeaderText="Türkçe" />

                <asp:BoundField DataField="Category" HeaderText="Kategori" />

                <asp:ImageField DataImageUrlField="PicturePath"
                    HeaderText="Resim"
                    ControlStyle-Width="100"
                    ControlStyle-Height="100" />

            </Columns>

        </asp:GridView>

        <div class="text-center mt-3">
            <a href="Dashboard.aspx">Panele Dön</a>
        </div>

    </div>

</div>
    </form>
</body>
</html>
