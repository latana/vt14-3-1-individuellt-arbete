<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GameList.aspx.cs" Inherits="WonderfulGames.Pages.GamePages.GameList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headplaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainplaceholder" runat="server">

    <%-- Visar en lista på alla spel --%>
    <asp:ListView ID="GameListView" runat="server"
        ItemType="WonderfulGames.Model.Game"
        DefaultMode="ReadOnly"
        SelectMethod="GameListView_GetData"
        DataKeyNames="GameID">
        <LayoutTemplate>
            <table>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
            <asp:DataPager ID="DataPager" runat="server" PageSize="18">
                <Fields>
                    <%-- Först sidan knappen --%>
                    <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                    <asp:NumericPagerField />
                    <%-- Sista sidan knappen --%>
                    <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                </Fields>
            </asp:DataPager>
        </LayoutTemplate>
        <ItemTemplate>
            <dl>
                <dt><%-- Länkar med spel som lopas fram --%>
                    <asp:HyperLink ID="HyperLink1" Class="game" runat="server" NavigateUrl='<%# GetRouteUrl("GameDetails", new { id = Item.GameID })  %>' Text='<%# Item.Title %>' />
                </dt>
            </dl>
        </ItemTemplate>
        <EmptyDataTemplate>
            <%-- Detta visas då kunder saknas i databasen. --%>
            <p>
                Spel saknas
            </p>
        </EmptyDataTemplate>
    </asp:ListView>

</asp:Content>
