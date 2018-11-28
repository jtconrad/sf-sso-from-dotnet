using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class LoadProperties
{
    public static String idpIssuerUrl;
    public static String certificate;
    public static String issuer;
    public static String assertionConsumerServiceUrl;
    public static void initProperties()
    {
        var data = new Dictionary<string, string>();
        foreach (var row in File.ReadAllLines("\\config.properties"))
        {
            String key = row.Split('=')[0];
            String value = row.Split('=')[1];
            data.Add(key, value);
        }
        idpIssuerUrl = data["idpIssuerUrl"];
        certificate = data["certificate"];
        issuer = data["issuer"];
        assertionConsumerServiceUrl = data["assertionConsumerServiceUrl"];
    }
}
