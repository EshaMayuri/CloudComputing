<%@ Page Title="OptimizedRoute" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OptmizedRoute.aspx.cs" Inherits="CloudComputing.OptmizedRoute" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
        <h1 style="margin-left:auto;margin-right:auto;">Optimized Route!</h1>
    </div>
    
    <asp:GridView ID="GridView2" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="1" ForeColor="#333333" GridLines="None" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" style="margin-left:auto; margin-right:auto;"  >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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
</asp:Content>
