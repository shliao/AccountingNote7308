﻿<%@ Page Title="" Language="C#" MasterPageFile="~/SystemAdmin/Admin.Master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="AccountingNote7308.SystemAdmin.UserDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>會員管理</h2>
    <table border="1">
        <tr><td align="center">帳號</td>
            <td><asp:TextBox ID="acctxtbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr><td align="center">名稱</td>
            <td><asp:TextBox ID="nametxtbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr><td align="center">Email</td>
            <td><asp:TextBox ID="emailtxtbox" runat="server"></asp:TextBox></td>
        </tr>
        <tr><td align="center">等級</td>
            <td><asp:DropDownList ID="ddl_Level" runat="server">
                <asp:ListItem Value="0">管理者</asp:ListItem>
                <asp:ListItem Value="1">一般會員</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr><td align="center">建立時間</td>
            <td><asp:Label ID="timeLabel" runat="server"></asp:Label></td>
        </tr>
        <tr><td colspan="2">
            <asp:Button ID="Savebtn" runat="server" Text="Save" OnClick="Savebtn_Click" />&nbsp;&nbsp;
            <asp:Button ID="Deletebtn" runat="server" Text="Delete" OnClick="Deletebtn_Click"/>
            </td>
        </tr>
    </table>
    <asp:Literal ID="LitMsg" runat="server"></asp:Literal><br />
    <asp:Button ID="pwdbtn" runat="server" Text="變更密碼" />
</asp:Content>
