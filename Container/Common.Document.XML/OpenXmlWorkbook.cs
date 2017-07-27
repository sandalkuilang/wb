using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace Common.Document.XML
{
    public class OpenXmlWorkbook : Workbook
    {
        private string filePath;
        private Stream fileStream; 
        private XSSFWorkbook workbook; 
        protected XSSFWorkbook Workbook
        {
            get
            {
                if (workbook == null & this.fileStream != null)
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                return workbook;
            }
        }

        public OpenXmlWorkbook(string path, SourceContent source)
            : base(path, source)
        {
            this.filePath = path;
        } 
     
        public OpenXmlWorkbook(Stream stream)
            : base(stream)
        {
            this.fileStream = stream;
            workbook = new XSSFWorkbook(stream);
        }

        public OpenXmlWorkbook(StreamReader stream)
            : base(stream)
        {
            this.fileStream = stream.BaseStream;
            workbook = new XSSFWorkbook(stream.BaseStream);
        }
         
        public override ExcelTabular this[int index]
        {
            get
            {
                return GetSheet(index);
            } 
        }
         
        public override System.IO.Stream FileStream
        {
            get
            {
                return fileStream;
            }
            protected set
            {
                fileStream = value;
            }
        }

        public override string[] GetSheetNames()
        {
            int numberSheets = Workbook.NumberOfSheets; 
            List<string> sheets = new List<string>();
            for (int i = 0; i < numberSheets - 1; i++)
            {
                sheets.Add(Workbook.GetSheetName(i));
            }
            return sheets.ToArray();
        }

        public override ExcelTabular GetSheet(string name)
        { 
            ISheet sheet =  Workbook.GetSheet(name);
            return new OpenXmlSheet(sheet);
        }

        public override ExcelTabular GetSheet(int index)
        { 
            ISheet sheet = Workbook.GetSheetAt(index);
            return new OpenXmlSheet(sheet); 
        }

        public override string GetSheetName(int index)
        {
            return workbook.GetSheetName(index);
        }

        public override void RemoveSheet(int index)
        {
            workbook.RemoveAt(index);
        }

        public override void RemoveSheet(string name)
        {
            int index = workbook.GetSheetIndex(name);
            workbook.RemoveAt(index);
        }

        public override void SetActiveSheet(string name)
        {
            int index = workbook.GetSheetIndex(name);
            workbook.SetActiveSheet(index);
        }

        public override void SetActiveSheet(int index)
        {
            workbook.SetActiveSheet(index);
        } 

        public override bool IsReadOnly
        {
            get
            {
                return workbook.IsReadOnly;
            }
        }
         
        public override byte[] Write()
        {
            MemoryStream memoryStream = new MemoryStream();
            workbook.Write(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
