using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Net;

namespace Common.Document
{
    public abstract class Workbook
    { 
        public abstract Stream FileStream { get; protected set; }

        public abstract bool IsReadOnly { get; } 

        public Workbook(string path, SourceContent source)
        { 
            if (source == SourceContent.Local)
            {
                File.SetAttributes(path, FileAttributes.Normal);
                this.FileStream = File.Open(path, FileMode.Open);
            }
            else
            {
                WebRequest req = WebRequest.Create(path);
                req.UseDefaultCredentials = true;
                req.PreAuthenticate = true;
                req.Credentials = CredentialCache.DefaultCredentials;
                
                WebResponse result = req.GetResponse();
                this.FileStream = result.GetResponseStream();
            } 
        }

        public Workbook(Stream stream)
        {
            this.FileStream = stream;
        }

        public Workbook(StreamReader stream)
        { 
            this.FileStream = stream.BaseStream;
        }

        protected byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }
         
        public abstract byte[] Write();
        public abstract ExcelTabular this[int index] { get; }
        public abstract string[] GetSheetNames();
        public abstract ExcelTabular GetSheet(string name);
        public abstract ExcelTabular GetSheet(int index); 
        public abstract string GetSheetName(int index);
        public abstract void RemoveSheet(int index);
        public abstract void RemoveSheet(string name);
        public abstract void SetActiveSheet(string name);
        public abstract void SetActiveSheet(int index);

    }

    public enum SourceContent
    {
        Local = 1,
        Url
    }
}
