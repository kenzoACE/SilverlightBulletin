<%@ WebHandler  Language="C#" Class="LiveGetName"%>

using System;
using System.Web;
using System.IO;

public class LiveGetName : IHttpHandler
{
    protected string IM_URL = "";
    protected string IM_NAME = "";
    string file = "";
    int index = 0;
    string line = "";
   
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
        
        //find the username corresponding to the IM address
        index = file.IndexOf(IM_URL);
        index = file.IndexOf("User name: ", index);
        index += 11;  //length of string "User name: "
        IM_NAME = file.Substring(index, file.IndexOf("\r\n", index) - index);
                
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

