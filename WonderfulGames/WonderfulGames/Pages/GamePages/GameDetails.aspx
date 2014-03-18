<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GameDetails.aspx.cs" Inherits="WonderfulGames.Pages.GamePages.GameDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headplaceholder" runat="server">
    <h2>Details</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainplaceholder" runat="server">

    <%-- ListViewn som visar detaljerna på varje spel--%>

    <asp:ListView ID="GameListView" runat="server"
        ItemType="WonderfulGames.Model.Game"
        OnItemDataBound="GameListView_ItemDataBound"
        DefaultMode="ReadOnly"
        SelectMethod="GameDetails_GetItem"
        DeleteMethod="GameListView_DeleteItem"
        DataKeyNames="GameID">
        <LayoutTemplate>

            <%-- Platshållare för spel --%>
            <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
        </LayoutTemplate>
        <ItemTemplate>
            <%-- Spelets uppgifter--%>
            <tr>
                <td><%-- Titel--%>
                    <div class="detaildiv">
                    <p class="fieldtext">Titel:</p>
                    <%-- Titel--%>
                    <asp:Label ID="Title" Class="fieldItem" runat="server" Text='<%#: Item.Title %>'></asp:Label>
                    </div>
                </td>
                <td>
                    <%-- Genre--%>
                    <div class="detaildiv">
                    <asp:MultiView ID="GameMultiView" runat="server" ActiveViewIndex="0">
                        <asp:View runat="server">
                            <p class="fieldtext">Genre:</p>
                            <asp:Label ID="GenreLabel" Class="fieldItem" runat="server" Text="{0} " />
                        </asp:View>
                    </asp:MultiView>
                        </div>
                </td>
                <td><%-- Developer--%>
                    <div class="detaildiv">
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View runat="server">
                            <p class="fieldtext">Utvecklare</p>
                            <asp:Label ID="DeveloperLabel" Class="fieldItem" runat="server" Text="{0} " />
                        </asp:View>
                    </asp:MultiView>
                        </div>
                </td>
                <td><%-- Released--%>
                    <div class="detaildiv">
                    <p class="fieldtext">Utgivet:</p>
                    <asp:Label ID="Released" runat="server" Class="fieldItem" Text='<%# Eval("Released", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </div>
                </td>
                <td><%-- Grade--%>
                    <div class="detaildiv">
                    <p class="fieldtext">Betyg</p>
                    <asp:Label ID="Grade" runat="server" Class="fieldItem" Text='<%#: Item.Grade %>'></asp:Label>
                        </div>
                </td>
                <td><%-- Edit knapp--%>
                    <asp:HyperLink ID="Edit" runat="server" Class="editgamebuttons" Text="Redigera" NavigateUrl='<%# GetRouteUrl("GameUpdate", new { id = Item.GameID }) %>' />
                </td>
                <td><%-- Delete knapp--%>
                    <asp:LinkButton ID="Delete" Class="editgamebuttons" runat="server" CommandName="Delete" Text="Ta Bort" CausesValidation="false"
                        OnClientClick='<%# String.Format("return confirm(\"Vill du verkligen ta bort {0}?\")", Item.Title) %>' />
                </td>
            </tr>

        </ItemTemplate>
        <EmptyDataTemplate>
            <%-- Detta visas då spelet saknas i databasen. --%>
            <p>
                Spelet saknas
            </p>
        </EmptyDataTemplate>
    </asp:ListView>

</asp:Content>
