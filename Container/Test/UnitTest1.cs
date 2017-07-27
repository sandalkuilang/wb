using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Database.SqlLoader;
using Common.Database;
using Common.Document.XML;
using Common.Document;
using System.IO;

namespace Test
{
    [TestClass]
    public class UnitTest1
    {
        //[TestMethod]
        public void TestMethod1()
        { 

            IFileLoader loader = new FileSqlLoader("file-sql-loader", AppDomain.CurrentDomain.BaseDirectory + @"\App_Code\SqlFiles");
            IDbManager dbManager = new Common.Database.Petapoco.PetapocoDbManager(null, null);

            //// initialize sql loader and connection descriptor
            dbManager.AddSqlLoader(loader);

            dbManager.ConnectionDescriptor.Add(new ConnectionDescriptor()
            {
                ConnectionString = @"DATA SOURCE=(DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = tcp)(HOST = batuampar.idbpn.chevronCommon.net)(PORT = 1521)))(CONNECT_DATA =(SID = ih10bpnd))); USER ID=MIMSWH; PASSWORD=mimswh4devel#2012;",
                IsDefault = true,
                ProviderName = "System.Data.OracleClient",
                Name = "MIMSWH"
            });

            IDataCommand db = dbManager.GetDatabase("MIMSWH");
            List<object> result = db.Query<object>("SELECT * FROM tddistrict", null);
        }

        [TestMethod]
        public void ReadExcel()
        {
            // C:\Users\!hpfr\Desktop\BookTest.xlsx
            OpenXmlWorkbook book = new OpenXmlWorkbook(@"C:\Users\!hpfr\Desktop\Template.xlsx", SourceContent.Local);
            OpenXmlSheet sheet = (OpenXmlSheet)book.GetSheet(0);
            sheet.Read();
            sheet.StartRow = 2;
            Random rnd = new Random(); 
            for(int i = 0; i < 10; i++)
            {
                sheet.CreateRow(i);
                sheet.CreateCell(i, 0, "value");
                //for (int column = 0; column < 4; column++)
                //{
                //    sheet.CreateCell(i, column, "value");
                    //sheet.CreateRow(sheet.StartRow + i, column, new OpenXmlSheet.WriteDataCellCallback(GetCurrentCell), new DataCell() 
                    //    {
                    //        Value = string.Format("hai-{0}", rnd.Next()),
                    //        Type = DataCellType.String,
                    //        ColumnIndex = columnIndex
                    //    });
                //}
            }
            byte[] readAllBytes = book.Write();  
            File.WriteAllBytes(@"C:\Users\!hpfr\Desktop\Output.xlsx", readAllBytes);
            //sheet.GetRows();
            //DataRow row = sheet.Rows[3]; 
        }

        //private int columnIndex;
        //public DataCell GetCurrentCell(object state)
        //{
        //    Random rnd = new Random(); 
        //    return new DataCell() 
        //    {
        //        Value = string.Format("hai-{0}", rnd.Next()),
        //        Type = DataCellType.String,
        //        ColumnIndex = columnIndex
        //    };
        //}

    }
}
