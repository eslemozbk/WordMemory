<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordDetail.aspx.cs" Inherits="WordMemoryWeb.WordDetail" %>

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

        <h3 class="text-center mb-4">Kelime Detayı</h3>

        <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>

        <div class="row">
            <div class="col-md-4 text-center">
                <asp:Image ID="imgWord" runat="server"
                    CssClass="img-fluid rounded border"
                    Width="250px"
                    Height="250px" />
            </div>

            <div class="col-md-8">
                <h2>
                    <asp:Label ID="lblEngWord" runat="server"></asp:Label>
                </h2>

                <h4 class="text-muted">
                    <asp:Label ID="lblTurWord" runat="server"></asp:Label>
                </h4>

                <p>
                    <strong>Kategori:</strong>
                    <asp:Label ID="lblCategory" runat="server"></asp:Label>
                </p>

                <hr />

                <h5>Örnek Cümleler</h5>

                <asp:Repeater ID="rptSamples" runat="server">
                    <ItemTemplate>
                        <div class="alert alert-light border">
                            <%# Eval("SampleSentence") %>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>

        <div class="text-center mt-4">
            <a href="WordList.aspx" class="btn btn-secondary">Listeye Dön</a>
            <a href="Dashboard.aspx" class="btn btn-primary">Panele Dön</a>
        </div>

    </div>
</div>
    </form>
</body>
</html>
