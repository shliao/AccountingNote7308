<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AccountingNote7308.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table colspan="2">
            <tr><td><h1>流水帳管理系統</h1></td><td>&nbsp&nbsp&nbsp&nbsp<a href="Default.aspx">返回主頁</a></td></tr>
        </table>
        <h3>登入</h3>
        <asp:PlaceHolder ID="plcLogin" runat="server">
        帳號<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
        <br />
        密碼<asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click"/>
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
        </asp:PlaceHolder>
    </form>
</body>
</html>
