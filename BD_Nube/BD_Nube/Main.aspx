<%@ Page Language="C#" AutoEventWireup="true" CodeFile="factura2.aspx.cs" Inherits="factura2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Facturas</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            color: #333;
            margin: 0;
            padding: 0;
            text-align: center;
        }

        header {
            background-color: #0074D9;
            color: white;
            padding: 20px;
        }

        h1 {
            margin: 0;
            font-size: 36px;
        }

        .container {
            display: flex;
            align-items: center;
            padding: 20px;
            justify-content: space-between;
        }

        .filters {
            display: flex;
        }

        .filter-select {
            width: 150px;
            padding: 5px;
            font-size: 14px; 
            margin-right: 10px; 
        }

        .buttons {
            text-align: right;
        }

        .btn {
            background-color: #0074D9;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-left: 10px;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        .gridview {
            background-color: #fff;
            border: 1px solid #ccc;
            border-collapse: collapse;
            width: 100%;
            margin-top: 20px;
        }

        .gridview th, .gridview td {
            padding: 10px;
            border: 1px solid #ccc;
        }

        .gridview th {
            background-color: #0074D9;
            color: white;
        }

        /* Estilo para filas impares (blanco) */
        .gridview tr:nth-child(odd) {
            background-color: #fff;
        }

        /* Estilo para filas pares (gris) */
        .gridview tr:nth-child(even) {
            background-color: #f2f2f2;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <h1>Facturas</h1>
        </header>
        <div class="container">
            <div class="filters">
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="FiltrarFacturas" AutoPostBack="true" CssClass="filter-select">
                </asp:DropDownList>
                <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="FiltrarFacturas" AutoPostBack="true" CssClass="filter-select">
                </asp:DropDownList>
                <asp:Button ID="btnResetFiltros" runat="server" Text="Restablecer Filtros" OnClick="ResetFiltros" CssClass="btn" />
            </div>
            <div class="buttons">
                <asp:Button ID="btnExport" runat="server" Text="Descargar" OnClick="ExportXLS" CssClass="btn" />
            </div>
        </div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="true" CssClass="gridview">
        </asp:GridView>
    </form>
</body>
</html>
