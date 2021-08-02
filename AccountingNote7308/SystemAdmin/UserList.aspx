<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>會員管理</h2>
    <table>
        <tr>
            <td>
                <asp:Button ID="addUserbtn" runat="server" Text="Button" /><br />
                <asp:GridView ID="GV_UserList" runat="server" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField HeaderText="帳號" />
                        <asp:BoundField HeaderText="名稱" />
                        <asp:BoundField HeaderText="Email" />
                        <asp:BoundField HeaderText="等級" />
                        <asp:BoundField HeaderText="建立時間" />
                        <asp:TemplateField HeaderText="Act">
                            <ItemTemplate>
                                <a href="UserDetail.aspx">Edit</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
