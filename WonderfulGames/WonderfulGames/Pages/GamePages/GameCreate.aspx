<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="GameCreate.aspx.cs" Inherits="WonderfulGames.Pages.GamePages.GameCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headplaceholder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainplaceholder" runat="server">

    <%-- Validation summory--%>
    <div id="felmeddelande">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Ett eller flera fel har inträffat!" ShowModelStateErrors="False" ValidationGroup="Insert" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Ett eller flera fel har inträffat!" />
    </div>

    <asp:FormView ID="GameListView" runat="server"
        ItemType="WonderfulGames.Model.Game"
        DefaultMode="Insert"
        RenderOuterTable="false"
        InsertMethod="GameFormView_InsertItem">
        <%-- InsertTemplate--%>
        <InsertItemTemplate>
            <tr>
                <td>
                    <div class="insertdiv">
                        <p class="fieldtext">Titel</p>
                        <%-- Validering för Title--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Titeln på spelet får inte vara tomt!"
                            Text="*" Display="Dynamic" ControlToValidate="TitleTextBox" ValidationGroup="Insert"></asp:RequiredFieldValidator>

                        <%-- Titel fält--%>
                        <asp:TextBox ID="TitleTextBox" Class="insertbox" runat="server" Text='<%# BindItem.Title %>' MaxLength="50" />
                    </div>
                </td>
                <td>
                    <div class="insertdiv">
                        <p class="fieldtext">Genre</p>
                        <%-- Dropdown-lista för Genre--%>
                        <asp:DropDownList ID="GenreDropDownList" Class="insertbox" runat="server"
                            ItemType="WonderfulGames.Model.Genre"
                            SelectMethod="GenreDropDownList_GetData"
                            DataTextField="GenreType"
                            DataValueField="GenreID"
                            SelectedValue='<%# BindItem.GenreID %>' />
                    </div>
                </td>
                <td>
                    <div class="insertdiv">
                        <%-- Dropdown-lista för Developer--%>
                        <p class="fieldtext">Utvecklare</p>
                        <asp:DropDownList ID="DeveloperDropDownList" Class="insertbox" runat="server"
                            ItemType="WonderfulGames.Model.Developer"
                            SelectMethod="DeveloperDropDownList_GetData"
                            DataTextField="DeveloperName"
                            DataValueField="DeveloperID"
                            SelectedValue='<%# BindItem.DeveloperID %>' />
                    </div>
                </td>
                <td>
                    <div class="insertdiv">
                        <p class="fieldtext">Utgivet</p>
                        <%-- Validering för Released--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Du måste ange en datum. tex 'yyyy-mm-dd'"
                            Text="*" Display="Dynamic" ControlToValidate="ReleasedTextBox" ValidationGroup="Insert"></asp:RequiredFieldValidator>

                        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Ange ett giltigt datum. tex 1992-02-28"
                            ControlToValidate="ReleasedTextBox" Display="Dynamic" Type="Date" MaximumValue="2025-12-31"
                            MinimumValue="1930-01-01" Text="*" ValidationGroup="Insert"></asp:RangeValidator>

                        <%-- Fält för Released--%>
                        <asp:TextBox ID="ReleasedTextBox" Class="insertbox" runat="server" Text='<%# BindItem.Released %>' MaxLength="10" />
                    </div>
                </td>
                <td>
                    <div class="insertdiv">
                        <%-- Dropdown-list för Grade. Denna hämtar inget från databasen --%>
                        <p class="fieldtext">Betyg</p>
                        <asp:DropDownList ID="GradeDropDownList" Class="insertbox" runat="server" Text='<%# BindItem.Grade %>'>
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
                    </div>
                </td>
                <td>
                    <%--Insert knappen--%>
                    <asp:LinkButton ID="LinkButton3" Class="editgamebuttons" runat="server" CommandName="Insert" Text="Spara" />

                    <%-- Rensa knappen--%>
                    <asp:LinkButton ID="LinkButton4" Class="editgamebuttons" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                </td>
            </tr>

        </InsertItemTemplate>
    </asp:FormView>
</asp:Content>
