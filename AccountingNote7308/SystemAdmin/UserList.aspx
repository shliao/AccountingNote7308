<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.UserList" %>

<%@ Register Src="~/UserControl/ucPager.ascx" TagPrefix="uc1" TagName="ucPager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>會員管理</h2>
    <table>
        <tr>
            <td>
                <asp:Button ID="addUserbtn" runat="server" Text="Add User" /><br />
                <asp:GridView ID="GV_UserList" runat="server" AutoGenerateColumns="False"
                    OnRowDataBound="GV_UserList_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField HeaderText="帳號" DataField="Account"/>
                        <asp:BoundField HeaderText="名稱" DataField="Name"/>
                        <asp:BoundField HeaderText="Email" DataField="Email"/>
                        <asp:TemplateField HeaderText="等級">
                            <ItemTemplate>
                                <asp:Literal ID="ltlLevel" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="建立時間" DataField="CreateDate"/>
                        <asp:TemplateField HeaderText="Act">
                            <ItemTemplate>
                                <a href="UserDetail.aspx">Edit</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                <asp:PlaceHolder ID="plc_noUser" runat="server" Visible="False">No User.</asp:PlaceHolder>
                <uc1:ucPager runat="server" id="ucPager" Url="UserList.aspx" PageSize="10"/>
            </td>
        </tr>
    </table>
</asp:Content>
