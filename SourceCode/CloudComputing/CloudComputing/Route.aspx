<%@ Page Title="Maps" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Route.aspx.cs" Inherits="CloudComputing.Route" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <div class="jumbotron">
        <h1 style="margin-left:auto;margin-right:auto;">Maps Route</h1>
    </div>
    <div style="text-align:center;padding-top:20px;">
        <asp:Button ID="BtnShowMaps" runat="server" OnClick="BtnShowMaps_Click" Text="Show the location on Maps" />
    </!--div>

    <div style="padding-top:20px;">
        <div id="dvMap" style="width: 1097px; height: 800px; margin-left: 127px;"></div>
    </div>



    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyBg_weOUun-dvJOfgcWVSCgnoce9UITwfA&callback=initMap"></script>
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
                alert("in loop")
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