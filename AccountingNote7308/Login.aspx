<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AccountingNote7308.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:PlaceHolder ID="plcLogin" runat="server">
        Account:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtAccount" runat="server"></asp:TextBox><br />
        Password:&nbsp;
        <asp:TextBox ID="txtPWD" runat="server" TextMode="Password"></asp:TextBox><br />
        <asp:Button ID="Loginbtn" runat="server" Text="Login" OnClick="Loginbtn_Click" /><br />
        <asp:Literal ID="LiteralMsg" runat="server"></asp:Literal>
        </asp:PlaceHolder>
</asp:Content>
