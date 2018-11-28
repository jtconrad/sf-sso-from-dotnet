using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ConsumeResponse samlResponse = new ConsumeResponse();
        samlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]);

        if (samlResponse.isAuthenticated())
        {
            Response.Redirect("home.aspx?UserID=" + samlResponse.getSubject() + "&UserName=" + samlResponse.getUsername());
        }
        else
        {
            Response.Write("SSO is failed!");
        }
    }
}

