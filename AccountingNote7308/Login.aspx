<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AccountingNote7308.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>登入</h3>
    <asp:PlaceHolder ID="plcLogin" runat="server">帳號<asp:TextBox ID="txtAccount" runat="server"></asp:TextBox>
        <br />
        密碼<asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btnLogin" runat="server" Text="登入" OnClick="btnLogin_Click" />
        <asp:Literal ID="ltlMsg" runat="server"></asp:Literal>
    </asp:PlaceHolder>
</asp:Content>
