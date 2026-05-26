<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="WordMemoryWeb.Report" %>

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

        <h3 class="text-center mb-4">Analiz Raporu</h3>

        <div class="row text-center mb-4">
            <div class="col-md-3">
                <div class="card p-3">
                    <h5>Toplam Soru</h5>
                    <asp:Label ID="lblTotal" runat="server" CssClass="fs-4 fw-bold"></asp:Label>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card p-3">
                    <h5>Doğru</h5>
                    <asp:Label ID="lblCorrect" runat="server" CssClass="fs-4 fw-bold text-success"></asp:Label>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card p-3">
                    <h5>Yanlış</h5>
                    <asp:Label ID="lblWrong" runat="server" CssClass="fs-4 fw-bold text-danger"></asp:Label>
                </div>
            </div>

            <div class="col-md-3">
                <div class="card p-3">
                    <h5>Başarı</h5>
                    <asp:Label ID="lblSuccess" runat="server" CssClass="fs-4 fw-bold text-primary"></asp:Label>
                </div>
            </div>
        </div>

        <h5>Kategori Bazlı Başarı</h5>

        <asp:GridView ID="gvCategoryReport"
            runat="server"
            AutoGenerateColumns="False"
            CssClass="table table-bordered table-hover mt-3"
            GridLines="None">

            <Columns>
                <asp:BoundField DataField="Category" HeaderText="Kategori" />
                <asp:BoundField DataField="TotalAnswers" HeaderText="Toplam Cevap" />
                <asp:BoundField DataField="CorrectAnswers" HeaderText="Doğru Cevap" />
                <asp:BoundField DataField="SuccessRate" HeaderText="Başarı %" />
            </Columns>

        </asp:GridView>

        <div class="text-center mt-4">
            <button type="button" class="btn btn-primary" onclick="window.print()">Raporu Yazdır</button>
            <a href="Dashboard.aspx" class="btn btn-secondary">Panele Dön</a>
        </div>

    </div>
</div>
    </form>
</body>
</html>
