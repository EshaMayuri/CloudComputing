<%@ Page Title="RouteOptimizer" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="RouteOptimizer.aspx.cs" Inherits="CloudComputing.RouteOptimizer" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <div> <!--class="jumbotron"-->
        <h1 class="text-center" style="color:#2a6496;">Add addresses here!</h1>
    </div>

    <h2 class="col-md-12"></h2>

    <div class="row">            
        <div class="col-md-12">
        <asp:GridView ID="GridView1" runat="server" ShowFooter="True" AutoGenerateColumns="False" CellPadding="1" GridLines="None" 
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowCommand="GridView1_RowCommand" style="margin-left:auto; margin-right:auto;" CssClass="table table-hover table-striped">            
            <Columns>
                
                <asp:TemplateField HeaderText="Request Id" SortExpression="RequestId"  ControlStyle-Width="0px">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" class="col-md-6"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("requestId") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="lbInsert" CommandName="Insert" runat="server" OnClick="Insert" class="btn btn-primary">Add Address</asp:LinkButton>
                    </FooterTemplate>

                <ControlStyle Width="100px"></ControlStyle>

                </asp:TemplateField>
                

                <asp:TemplateField HeaderText="Address" SortExpression="Address">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" class="col-form-label" Text='<%# Bind("address") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("address") %>'></asp:Label>
                    </ItemTemplate>
                    
                    <FooterTemplate>
                        <asp:TextBox ID="txtAddress" runat="server" class="col-md-6 col-form-label"></asp:TextBox>
                        
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>
            
        </asp:GridView>
        </div>
   
    </div>

    <div class="row"> 
        <div class="col-md-12" style="text-align:center; color:#116779">    
            <h4>-----OR-----</h4>                  
        </div>
    </div>

    <div class="row" style="margin-left:41%">
        <div class="col-md-12 text-center">
            <asp:FileUpload ID ="FileUpload" runat="server" Display="Dynamic"/>
            &nbsp;            
        </div>
    </div>
    
    <div class="row">
        <div class="col-md-12 text-center">
           <asp:Button ID="BtnXMLData" runat="server" OnClick="BtnLoadData_Click" Text="Load Data" class="btn btn-primary"/>
            <asp:Label ID="lblMessage" runat="server" Font-Bold="true"></asp:Label>
        </div>
    </div>

    <div class="row"> 
        <div class="col-md-12">                      
            <h2></h2>
        </div>
    </div>
    <div class="row"> 
        <div class="col-md-12">                      
            <h2></h2>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12 text-center"> 
            
            <asp:Label ID="Label2" runat="server" Text="No. of Potholes to fix" class="col-md-6 col-form-label"></asp:Label>
            <asp:TextBox ID="txtCount" runat="server" class="col-md-6 col-form-label"></asp:TextBox>
            
            
        </div>
    </div>

    <div class="row"> 
        <div class="col-md-12">    
            <h2></h2>                  
        </div>
    </div>

    <div class="row text-center">            
        <div class="col-md-12">
            <asp:Button ID="Button1" runat="server" Text="Calculate Shortest Path" OnClick="CalculateShortestPath" class="btn btn-primary btn-lg"  style="margin-left:auto; margin-right:auto;"/>            
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
    </div>
</asp:Content>