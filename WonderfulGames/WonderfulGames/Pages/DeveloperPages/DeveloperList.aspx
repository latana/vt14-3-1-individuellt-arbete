<%@ Page Title="" Language="C#" MasterPageFile="~/Pages/Shared/Site.Master" AutoEventWireup="true" CodeBehind="DeveloperList.aspx.cs" Inherits="WonderfulGames.Pages.DeveloperPages.DeveloperList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headplaceholder" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="mainplaceholder" runat="server">

    <%-- Validerings pressentationen--%>
    <div id="felmeddelande">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Ett eller flera fel har inträffat!" ValidationGroup="Insert" ShowModelStateErrors="False" />
        <asp:ValidationSummary ID="ValidationSummary2" runat="server" HeaderText="Ett eller flera fel har inträffat!" ValidationGroup="Uppdate" ShowModelStateErrors="False" />
        <asp:ValidationSummary ID="ValidationSummary3" runat="server" HeaderText="Ett eller flera fel har inträffat!" />
    </div>
    <%-- Listan med utvecklarnas namn--%>

    <asp:ListView ID="DeveloperListView" runat="server"
        ItemType="WonderfulGames.Model.Developer"
        SelectMethod="DeveloperListView_GetData"
        InsertMethod="DeveloperListView_InsertItem"
        DeleteMethod="DeveloperListView_DeleteItem"
        UpdateMethod="DeveloperListView_UpdateItem"
        DataKeyNames="DeveloperID"
        OnItemDataBound="DeveloperListView_ItemDataBound"
        InsertItemPosition="FirstItem">
        <%-- Layouten --%>
        <LayoutTemplate>
            <table>
                <tr>
                    <th>Namn
                    </th>
                </tr>
                <asp:PlaceHolder ID="itemPlaceholder" runat="server" />
            </table>
            <asp:DataPager ID="DataPager1" runat="server" PageSize="20">
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
            <tr>
                <%-- Utvecklarna som lopas fram--%>
                <td>
                    <asp:Label ID="developer" runat="server" Text='<%#: Item.DeveloperName %>'></asp:Label>
                </td>
                <td><%-- Edit knappen. Tar oss till edititemtemplet--%>
                    <asp:LinkButton ID="EditLinkButton" Class="editdeveloperbuttons" runat="server" CommandName="Edit" Text="Redigera" CausesValidation="false" />
                </td>
                <td><%-- Delete knappen. Frågar ytterligare med en popup ruta--%>
                    <asp:LinkButton ID="DeleteLinkButton" Class="editdeveloperbuttons" runat="server" CommandName="Delete" Text="Ta Bort" CausesValidation="false"
                        OnClientClick='<%# String.Format("return confirm(\"Vill du verkligen ta bort {0}? \")", Item.DeveloperName) %>' />
                </td>
            </tr>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table class="empty">
                <tr>
                    <td>
                        <p>Utvecklare saknas.</p>
                    </td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <%-- Insert Utvecklare --%>
        <InsertItemTemplate>
            <tr>
                <td><%-- Validering DeveloperName Insert--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Namnet får inte vara tomt!"
                        Text="*" Display="Dynamic" ControlToValidate="insertDeveloperName" ValidationGroup="Insert"></asp:RequiredFieldValidator>

                    <%-- DeveloperName Insert Fält--%>
                    <asp:TextBox ID="insertDeveloperName" runat="server" Text='<%# BindItem.DeveloperName %>' ValidationGroup="Insert" />
                </td>
                <td>
                    <%-- Spara knappen--%>
                    <asp:LinkButton ID="Savebutton" Class="editdeveloperbuttons" runat="server" CommandName="Insert" Text="Spara" ValidationGroup="Insert" />

                    <%-- Avbrytknappen--%>
                    <asp:LinkButton ID="Cancelbutton" Class="editdeveloperbuttons" runat="server" CommandName="Cancel" Text="Rensa" CausesValidation="false" />
                </td>
            </tr>

        </InsertItemTemplate>
        <%-- Uppdatera Utvecklare --%>
        <EditItemTemplate>
            <tr>
                <td>
                    <%-- Validering Uppdatera Utvecklare --%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Namnet får inte vara tomt!"
                        Text="*" Display="Dynamic" ControlToValidate="editDeveloperName" ValidationGroup="Uppdate"></asp:RequiredFieldValidator>

                    <%-- Uppdatera Utvecklare Fält--%>
                    <asp:TextBox ID="editDeveloperName" runat="server" Text='<%# BindItem.DeveloperName %>' ValidationGroup="Uppdate" />
                </td>
                <td>
                    <%-- Update knapp --%>
                    <asp:LinkButton ID="LinkButton1" Class="editdeveloperbuttons" runat="server" CommandName="Update" Text="Spara" ValidationGroup="Uppdate" />

                    <%-- Avbryt knapp --%>
                    <asp:LinkButton ID="LinkButton2" Class="editdeveloperbuttons" runat="server" CommandName="Cancel" Text="Avbryt" CausesValidation="false" />
                </td>
            </tr>
        </EditItemTemplate>

    </asp:ListView>
</asp:Content>
