<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AccountingNote7308.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table colspan="2">
            <tr><td><h1>流水帳管理系統</h1></td><td>&nbsp&nbsp&nbsp&nbsp<a href="Login.aspx">登入系統</a></td></tr>
        </table>
        <br />
        <br />
  <table>
                        <tr>
                            <th>初次記帳</th>
                            <td>
                                <asp:Literal ID="firstdatalb" runat="server"></asp:Literal>
                            </td>
                        </tr>

                        <tr>
                            <th>最後記帳</th>
                            <td>
                                <asp:Literal ID="enddatalb" runat="server"></asp:Literal>
                            </td>
                        </tr>

                        <tr>
                            <th>記帳數量</th>

                            <td>
                                <asp:Literal ID="datacountlb" runat="server"></asp:Literal>
                            </td>
                        </tr>

                         <tr>
                            <th>會員數</th>
                            <td>
                                <asp:Literal ID="userlb" runat="server"></asp:Literal>
                            </td>
                        </tr>

              </table>
    </form>
</body>
</html>
