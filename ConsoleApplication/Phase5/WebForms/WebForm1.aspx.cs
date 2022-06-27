using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace WebForms
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public XmlElement GetRssFeed(string str)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(str);
            return doc.DocumentElement;
        }

        [WebMethod]
        public static string test(string querytype)
        {
            return "{test:\'test\'}";
        }

        [System.Web.Services.WebMethod]
        public static string GetPageMethod()
        {
            return "Welcome PageMethods";
        }

        [System.Web.Services.WebMethod]
        public static string GetStatus(string s1, string s2)
        {
            return (string)"Test";
        }
    }
}