<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="CloudComputing._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-left:auto;margin-right:auto;color:#2a6496; text-align:center">
        <h1>Route Optimization for Pothole Repair</h1>
    </div>
    <div>
        <p class="lead" style="text-align:center;">Web application that provides crew an interface to add list of addresses and generate an optimized route.</p>
        <!--<p><a href="http://www.asp.net" class="btn btn-primary btn-lg">Learn more &raquo;</a></p>-->
    </div>
    <div style="padding-left:20%">
        <asp:Image ID="img1" runat="server" ImageUrl='image.png'></asp:Image>
    </div>
</asp:Content>
