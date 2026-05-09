<%@ WebHandler Language="C#" Class="BullitenHandler" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.IO;

public class BullitenHandler : IHttpHandler
{

        public void ProcessRequest(HttpContext context)
        {
            StreamReader sr2 = File.OpenText(context.Server.MapPath("~/ClientBin/" + "Bulliten.txt"));

            //send back the bulliten file
            context.Response.Write(sr2.ReadToEnd());
            sr2.Close();
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }

