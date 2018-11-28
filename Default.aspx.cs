using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       
    }
   
    protected void Button1_Click(object sender, EventArgs e)
    {
        
        String user =  TextBox1.Text;
        String pass =   TextBox2.Text;

        if (user.Equals("username", StringComparison.OrdinalIgnoreCase) && pass.Equals("password"))
        {
            Response.Redirect("home.aspx?UserID="+user);
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
       

        AuthnRequest req = new AuthnRequest();

        Response.Redirect(LoadProperties.idpIssuerUrl + "?SAMLRequest=" + Server.UrlEncode(req.GetRequest()));
    }
   
}

