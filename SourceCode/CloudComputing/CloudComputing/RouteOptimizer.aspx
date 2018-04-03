<%@ Page Title="RouteOptimizer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RouteOptimizer.aspx.cs" Inherits="CloudComputing.RouteOptimizer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <div class="jumbotron">
        <h1 style="margin-left:auto;margin-right:auto;">Add addresses here!</h1>
    </div>
        <asp:GridView ID="GridView1" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="1" ForeColor="#333333" GridLines="None" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" style="margin-left:auto; margin-right:auto;"  >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Request Id" SortExpression="RequestId"  ControlStyle-Width="0px" >
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" style="margin-left: 0px; margin-right:0px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("requestId") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="lbInsert" CommandName="Insert" runat="server" ForeColor="White" OnClick="Insert">Add Address</asp:LinkButton>
                    </FooterTemplate>

                <ControlStyle Width="100px"></ControlStyle>

                </asp:TemplateField>

                <asp:TemplateField HeaderText="Address" SortExpression="Address"  ControlStyle-Width="500px">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" style="margin-left: 0px; margin-right:0px;" Text='<%# Bind("address") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                    </ItemTemplate>
                    
                    <FooterTemplate>
                        <asp:TextBox ID="txtAddress" runat="server" style="margin-left: 0px; margin-right:0px; width:1000px"></asp:TextBox>
                        
                    </FooterTemplate>
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

    <div style="padding-left:30%;padding-right:20%;padding-top:10px;" >
    <asp:Label ID="Label2" runat="server" Text="Number of Potholes to be Repaired(Enter 0 if all): "></asp:Label>
    <asp:TextBox ID="txtCount" runat="server" style="margin-left: 0px; margin-right:0px; width:100px"></asp:TextBox>
    </div>
    <div style="padding-left:40%;padding-right:70%;padding-top:10px; width:100px; ">
    <asp:Button ID="Button1" runat="server" Text="Calculate Shortest Path" OnClick="CalculateShortestPath" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
    </div>
    <div style="padding-left:40%;padding-right:70%;padding-top:10px; width:100px; ">
    </div>
    </div>

    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js">

        google.maps.event.addDomListener(window, 'load', function () {
            var options = {
                types: ['(cities)'],
                componentRestrictions: { country: "usa" }
            };
            var input = document.getElementById('address');
            var places = new google.maps.places.Autocomplete(input, options);
        });
    </script>
</asp:Content>