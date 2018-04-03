<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="CloudComputing.About" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Route Optimization for Pothole Repair</h2> 
    
    <h4><u>About the application.</u></h4>
    
    <p>Route Optimization for Pothole Repair is a web application that is used to assist the crew members to optimize route taken for pothole repair. Each city would have several potholes and multiple crews would be working on repairing those potholes. Each day the crew would be given a list of potholes to be repaired and the crew would head out to work on those potholes without any route optimization. This is an inefficient process, which results in wastage of time and fuel. The web application would help the crew to generate an optimized route that saves time and fuel. This optimized route will be generated using Travelling salesman algorithm.</p>

    <h4><u>Features of the application:</u></h4>
        <ul>
            <li>The web application would provide an option to add the list addresses of potholes that needs a repair.</li>
            <li>The application would give the crew an option to select the number of potholes they are willing to repair that day.</li>
            <li>Once the crew finalizes on the number of potholes they are willing to repair they can request the optimized route for those potholes on a click.</li>
        </ul>
    <h4><u>Tools and Technologies:</u></h4>
        <ul>
            <li><h5><u>Programming Languages: </u> ASP.Net</h5></li>
            <li><h5><u>Tools & Technologies: </u> Microsoft Visual Studio, HTML, CSS, Javascript</h5></li>
            <li><h5><u>Cloud Service: </u> AWS</h5></li>
            <li><h5><u>Source control: </u> GitHub</h5></li>
        </ul>
</asp:Content>
