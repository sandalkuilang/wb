/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPlatform.Credential;
using System.Text;
using System.IO;
using System.Reflection;
using Krokot.Database;
using WebPlatform.Menu;

namespace WebPlatform
{
    [Authorization]
    public abstract class PageController : BaseController
    {  
        public PageController()
            : base()
        {
            
        }

        public PageController(string title)
            : base(title)
        {
            
        }

        public virtual ActionResult Index()
        {
            if (Authentication.IsValid)
            { 
                this.Startup(); 
            }
            return View();
        }
         
        protected void CreateResponse(byte[] bytes, string contentType, string filename)
        {
            Response.ContentType = contentType;
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.xlsx", filename));
            Response.Clear();
            Response.BinaryWrite(bytes);
            Response.End();
        }

        protected void CreateResponse(Stream stream, string contentType, string filename)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            Response.ContentType = contentType;
            Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", filename));
            Response.Clear();
            Response.BinaryWrite(bytes);
            Response.End();
        }

        protected Stream GetManifestResourceStream(Assembly assm, string name)
        {
            Stream manifest  = null;
            foreach (string manifestName in assm.GetManifestResourceNames())
            {
                if (manifestName.Contains(name))
                {
                    manifest = assm.GetManifestResourceStream(manifestName);
                    break;
                }
            } 
            return manifest;
        }

        protected Stream GetManifestResourceStream(string name)
        {
            return GetManifestResourceStream(Assembly.GetCallingAssembly(), name);
        }

        protected void CreateExcelResponse(byte[] bytes, string filename)
        {
            CreateResponse(bytes, MIMEype.xls, filename);
        }

    }

    public class MIMEype
    {
        public const string docx = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        public const string doc = "application/msword";
        public const string xls = "application/vnd.ms-excel";
        public const string pdf = "application/pdf";
        public const string xlsx = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }
}