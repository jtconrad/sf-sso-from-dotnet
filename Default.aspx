<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Salesforce Identity Demo</title>
</head>
<body style="text-align: center">
    <form id="form2" runat="server">
    <div>
    <p>
        &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
                &nbsp;</p>
            <p>
        <asp:Label ID="Label1" runat="server" 
            Text="Sample Application to demo SAML based SSO with Salesforce Identity" 
            
                    style="font-weight: 700; text-align: center; font-family: Arial, Helvetica, sans-serif; font-size: x-large;"></asp:Label>
    
    &nbsp;
    </p></div>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Username" 
            style="font-family: Arial, Helvetica, sans-serif"></asp:Label>
    &nbsp;
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </p>
    <p>
        <asp:Label ID="Label3" runat="server" Text="Password" 
            style="font-family: Arial, Helvetica, sans-serif"></asp:Label>  &nbsp;
        <asp:TextBox ID="TextBox2" runat="server" type="password" TextMode="Password"></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="Button1" runat="server" Text="Login" onclick="Button1_Click" /> &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Login with Salesforce" Width="231px" />
    </p>
    </form>
</body>
</html>