<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GameUpdate.aspx.cs" Inherits="WonderfulGames.Pages.GamePages.GameUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headplaceholder" runat="server">
    <h2>Update</h2>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainplaceholder" runat="server">

    <%-- Validation summory--%>
    <div id="felmeddelande">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowModelStateErrors="False" HeaderText="Ett eller flera fel inträffade" ValidationGroup="Edit" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" />
    </div>
    <%-- Uppdatera kontakt --%>

    <asp:FormView ID="GameListView" runat="server"
        ItemType="WonderfulGames.Model.Game"
        DataKeyNames="GameId"
        DefaultMode="Edit"
        RenderOuterTable="false"
        SelectMethod="GameListView_GetItem"
        UpdateMethod="GameFormView_UpdateItem">
        <EditItemTemplate>
            <tr>
                <div class="editdiv">
                    <td><%-- Validering för Edit Title --%>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Spelets titel får inte vara tomt!"
                            Text="*" Display="Dynamic" ControlToValidate="EditTitel" ValidationGroup="Edit"></asp:RequiredFieldValidator>

                        <%-- Edit Titel fältet--%>
                        <asp:TextBox ID="EditTitel" Class="editboxes" runat="server" Text='<%# BindItem.Title %>' />
                    </td>
                    <td>
                        <%-- Edit Genre dropdownlist--%>
                        <asp:DropDownList ID="GenreDropDownList" Class="editboxes" runat="server"
                            ItemType="WonderfulGames.Model.Genre"
                            SelectMethod="GenreDropDownList_GetData"
                            DataTextField="GenreType"
                            DataValueField="GenreID"
                            SelectedValue='<%# BindItem.GenreID %>' />
                    </td>
                    <td>
                        <%-- Edit Developer dropdownlist--%>
                        <asp:DropDownList ID="DeveloperDropDownList" Class="editboxes" runat="server"
                            ItemType="WonderfulGames.Model.Developer"
                            SelectMethod="DeveloperDropDownList_GetData"
                            DataTextField="DeveloperName"
                            DataValueField="DeveloperID"
                            SelectedValue='<%# BindItem.DeveloperID %>' />
                    </td>
                    <td>
                        <%-- Validering för Edit Released --%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Du måste ange en datum T.E.X 'yyyy-mm-dd'"
                            Text="*" Display="Dynamic" ControlToValidate="EditReleased" ValidationGroup="Edit"></asp:RequiredFieldValidator>

                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Ange ett giltigt datum. tex 1992-02-28"
                            ControlToValidate="EditReleased" Display="Dynamic" Type="Date" MaximumValue="2025-12-31"
                            MinimumValue="1930-01-01" Text="*" ValidationGroup="Edit"></asp:RangeValidator>

                        <%-- Edit Released fältet --%>
                        <asp:TextBox ID="EditReleased" runat="server" Class="editboxes" Text='<%# Bind("Released", "{0:yyyy-MM-dd}") %>'></asp:TextBox>
                    </td>
                    <td><%-- Edit Grade dropdownlist. Hämtar inget från databasen--%>
                        <asp:DropDownList ID="GradeDropDownList" Class="editboxes" runat="server" Text='<%# BindItem.Grade %>'>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                            <asp:ListItem>6</asp:ListItem>
                            <asp:ListItem>7</asp:ListItem>
                            <asp:ListItem>8</asp:ListItem>
                            <asp:ListItem>9</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </div>
                <td>
                    <%-- Edit knappen--%>
                    <asp:LinkButton ID="Editbutton" Class="editgamebuttons" runat="server" CommandName="Update" Text="Spara" />

                    <%-- Avbryt knappen--%>
                    <asp:HyperLink ID="Canclebutton" Class="editgamebuttons" runat="server" NavigateUrl='<%# GetRouteUrl("GameDetails", new { id = Item.GameID })  %>' Text="Avbryt" />
                </td>
            </tr>
        </EditItemTemplate>
    </asp:FormView>

</asp:Content>
