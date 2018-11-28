<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Salesforce Identity Demo</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial, Helvetica, sans-serif;
        }
        .style2
        {
            font-size: x-large;
        }
    </style>
</head>
<body>
    <form id="form2" runat="server">
    <div style="text-align: center">
            <br />
            <b>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <span class="style1"><span class="style2">Welcome!<br />
            Federation ID:
        <asp:Label ID="Label1" runat="server" ></asp:Label> 
            <br />
            Salesforce User Name:
        <asp:Label ID="Label2" runat="server" ></asp:Label> 
            </span></span></b></div>
    </form>
</body>
</html>
