<%@ Page Title="Maps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Route.aspx.cs" Inherits="CloudComputing.Route" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
        <h1 class="text-center">Maps Route</h1>
    </div>
    <div class="row">
        <div class="col-md-12 text-center">
            <asp:Button ID="BtnShowMaps" runat="server" OnClick="BtnShowMaps_Click" Text="Show the location on Maps" class="btn btn-primary btn-lg"/>
        </div>
    </div>

    <div class="row">
        <div id="dvMap" style=" height: 800px;" class="col-md-12"></div>
    </div>



    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBYF1u3tTYq6RzlH0Gh75MIzv8w6WhHl5A&callback=initMap"></script>
    <script type="text/javascript">
        var markers = [
            <asp:Repeater ID="rptMarkers" runat="server">
                <ItemTemplate>
                    {
                    "title": '<%# Eval("id") %>',
                    "lat": '<%# Eval("lat") %>',
                    "lng": '<%# Eval("lng") %>',
                    "description": '<%# Eval("addr") %>'
                }
        </ItemTemplate>
                <SeparatorTemplate>
                    ,
        </SeparatorTemplate>
            </asp:Repeater >
    ];
    </script>


    <script type="text/javascript">
        window.onload = function () {
            directionsDisplay = new google.maps.DirectionsRenderer();
            directionsService = new google.maps.DirectionsService();
            var mapOptions = {
                center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                zoom: 8,
                mapTypeId: google.maps.MapTypeId.ROADMAP
            };
            directionsDisplay.setMap(mapOptions);
            var infoWindow = new google.maps.InfoWindow();
            var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
            var request = {
                travelMode: 'DRIVING'
            };
            for (i = 0; i < markers.length; i++) {
                var data = markers[i]
                var myLatlng = new google.maps.LatLng(data.lat, data.lng);
                var marker = new google.maps.Marker({
                    position: myLatlng,
                    map: map,
                    title: data.title
                });
                //alert("in loop")
                google.maps.event.addListener(marker, "click", (function (marker, i) {
                    infoWindow.setContent(data.description);
                    infoWindow.open(map, marker);
                })(marker, i));


                if (i == 0) {
                    request.origin = new google.maps.LatLng(data.lat, data.lng);
                }
                else if (i == markers.length - 1) {
                    request.destination = new google.maps.LatLng(data.lat, data.lng);
                }

                else {
                    if (!request.waypoints) request.waypoints = [];
                    request.waypoints.push({
                        location: new google.maps.LatLng(data.lat, data.lng),
                        stopover: true
                    });
                }
            }
            directionsService.route(request, function (response, status) {
                if (status == google.maps.DirectionsStatus.OK) {
                    directionsDisplay.setDirections(response);
                }
            });
            directionsDisplay.setMap(map);
        }
    </script>

</asp:Content>