<%@ WebHandler Language="C#" Class="LiveGetID" %>

using System;
using System.Web;
using System.IO;

public class LiveGetID : IHttpHandler
{
    protected string IM_URL = "";
    protected string IM_NAME = "";
    string file = "";
    int index = 0;

    public void ProcessRequest(HttpContext context)
    {
        //read the IM address uploaded
        StreamReader sr = new StreamReader(context.Request.InputStream);

        IM_URL = sr.ReadToEnd();
        sr.Close();

        if (String.IsNullOrEmpty(IM_URL))
        {
            context.Response.Write("ERROR");
            return;
        }
        
        //read the Live ID list file
        StreamReader sr2 = File.OpenText(context.Server.MapPath("~/LiveID/ClientBin/" + "Live_ID_list.txt"));
        file = sr2.ReadToEnd();
        sr2.Close();

        index = file.IndexOf(IM_URL);

        index -= 13;
        
        //find the username corresponding to the IM address
        IM_NAME = file.Substring(index, 4);  //ID is 32 characters

        context.Response.Write(IM_NAME);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
