<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrorPage.aspx.cs" Inherits="WonderfulGames.ErrorPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h1>Serverfel</h1>

        <p>Ett allvarligt fel har uppstått. Vi ber så hemskt mycket om ursäkt och skall rätta till detta så snart som möjligt.
        </p>
        <asp:HyperLink ID="HyperLink1" runat="server" Text="Tillbaka till WonderfulGames" NavigateUrl="<%$ RouteUrl:routename=GameList %>" />

    </div>
    </form>
</body>
</html>
