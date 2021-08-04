<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="AccountingList.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.AccountingList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr> 
           <td>
            <asp:Button ID="btnCreate" runat="server" Text="Add Accounting" OnClick="btnCreate_Click" />
             
            <asp:GridView ID="gvAccountingList" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAccountingList_RowDataBound" >
            <Columns>
                <asp:BoundField HeaderText="標題" DataField="Caption"/>
                <asp:BoundField HeaderText="金額" DataField="Amount"/>
                <asp:TemplateField HeaderText="In/Out">
                    <ItemTemplate>
                        <asp:Label ID="lblActType" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField HeaderText="建立日期" DataField="CreateDate" DataFormatString="{0:yyyy-MM-dd}"/>
                <asp:TemplateField HeaderText="編輯">
                    <ItemTemplate>
                        <a href="/SystemAdmin/AccountingDetail.aspx?ID=<%# Eval("ID") %>">Edit</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           </asp:GridView>          
                    <asp:PlaceHolder ID="plcNoData" runat="server" Visible="false">
                        <p style="color:red">
                            此帳號沒有任何流水帳紀錄
                        </p>
                    </asp:PlaceHolder>
                 </td>
               </tr> 
        </table>
</asp:Content>
