<%@ WebHandler Language="C#" Class="WM" %>

using System.Web;
using System.IO;
using System;
using System.Text;


/// <summary>
/// Summary description for WM
/// </summary>
public class WM : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string temp = "";

        char[] charArray = new char[short.MaxValue];
        string encodedText = "";
        string resultText = "";

        StreamReader sr2 = File.OpenText(context.Server.MapPath("~/ClientBin/" + "Bulliten.txt"));

        temp = sr2.ReadToEnd();
        sr2.Close();

        resultText = temp.Replace("\r\n", "");

        charArray = resultText.ToCharArray();

        for (int x = 0; x < charArray.Length; x++)
        {
            encodedText = encodedText.Insert(encodedText.Length, Convert.ToString((int)charArray[x])) + "-";
        }

        File.WriteAllText(context.Server.MapPath("~/ClientBin/" + "WMBulliten.txt"), encodedText);
        
        //send back the bulliten file
        context.Response.ContentType = "text/plain";
        context.Response.Write("");
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
