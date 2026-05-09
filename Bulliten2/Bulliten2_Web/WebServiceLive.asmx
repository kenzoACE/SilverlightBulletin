<%@ WebService Language="C#" Class="WebServiceLive" %>

using System.Web;
using System.Web.Services;

public class WebServiceLive : System.Web.Services.WebService
{
    protected string IM_URL = "";
    protected string IM_NAME = "";

    [WebMethod(EnableSession = true)]
    public string HelloWorld()
    {
        IM_NAME = (string)HttpContext.Current.Session["Id"];
        return IM_NAME;
    }
}