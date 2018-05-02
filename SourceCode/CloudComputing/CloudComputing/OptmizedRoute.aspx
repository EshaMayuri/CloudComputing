<%@ Page Title="OptimizedRoute" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OptmizedRoute.aspx.cs" Inherits="CloudComputing.OptmizedRoute" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div>
        <h1 class="text-center" style="color:#2a6496;">Optimized Route!</h1>
    </div>
    
    <asp:GridView ID="GridView2" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="1" GridLines="None" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" style="margin-left:auto; margin-right:auto;"  CssClass="table table-hover table-striped" >
            
            <Columns>
                <asp:TemplateField HeaderText="Id" SortExpression="Id"  ControlStyle-Width="0px" >
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 0px; margin-right:0px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                <ControlStyle Width="100px"></ControlStyle>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Address" SortExpression="Address"  ControlStyle-Width="500px">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" style="margin-left: 0px; margin-right:0px;" Text='<%# Bind("address") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>           
    </asp:GridView>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="BtnShowMaps" runat="server" OnClick="BtnShowMaps_Click" Text="Show the location on Maps" class="btn btn-primary btn-lg"/>
        </div>
    </div>
</asp:Content>
