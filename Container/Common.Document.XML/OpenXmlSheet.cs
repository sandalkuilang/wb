using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO; 
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace Common.Document.XML
{
    public class OpenXmlSheet : ExcelTabular
    {
        private ISheet sheet;
        private DataRow[] rows; 
        private IRow currentRow;

        public delegate DataCell WriteDataCellCallback(object state);

        public OpenXmlSheet(Object value) : base(value)
        {
            sheet = (ISheet)value; 
        }

        public override DataRow[] Rows
        {
            get
            {
                return rows;
            }
            protected set
            {
                rows = value;
            }
        }
         
        public override IDictionary<int, DataRow> GetRows()
        {
            IDictionary<int, DataRow> rowsDictionary = new Dictionary<int, DataRow>();
            
            DataRow row;
            for (int i = this.StartRow; i < sheet.LastRowNum; i++)
            {
                row = GetRow(i);
                rowsDictionary.Add(i, row);
            }
            rows = rowsDictionary.Values.ToArray();
            
            return rowsDictionary;
        }

        public override DataRow GetRow(int index)
        {
            DataRow dataRow = new DataRow();
            currentRow = sheet.GetRow(index);
            dataRow.Cells = new Dictionary<int, DataCell>();
            dataRow.Index = index;
            if (currentRow != null)
            {
                StartHeader = currentRow.FirstCellNum;
                for (int i = StartHeader; i < currentRow.Cells.Count; i++)
                {
                    dataRow.Cells.Add(i, GetCell(i));
                }
            }
            else
            {
                dataRow.Cells.Add(0, new DataCell());
            }
            return dataRow;
        }

        public override DataCell GetCell(int index)
        {
            return GetCell(currentRow, index);
        }

        private DataCell GetCell(IRow row, int index)
        {
            DataCell retCell = new DataCell();
            ICell cell = row.Cells[index]; 
            switch (cell.CellType)
            {
                case CellType.String :
                    retCell.Type = DataCellType.String;
                    retCell.Value = cell.StringCellValue;
                    break;
                case CellType.Numeric :
                    retCell.Type = DataCellType.Numeric;
                    retCell.Value = cell.NumericCellValue;
                    break;
                case CellType.Formula :
                    retCell.Type = DataCellType.Formula;
                    retCell.Value = cell.CellFormula;
                    break;
                case CellType.Boolean :
                    retCell.Type = DataCellType.Boolean;
                    retCell.Value = cell.BooleanCellValue;
                    break;
                case CellType.Blank :
                    retCell.Type = DataCellType.Blank;
                    retCell.Value = cell.StringCellValue;
                    break;
            }
            retCell.ColumnIndex = cell.ColumnIndex; 
            return retCell;
        }
          
        public override void InsertRow(DataCell[] cells)
        {
            throw new NotImplementedException();
        }

        public override void InsertRow(int index, DataCell[] cells)
        {
            throw new NotImplementedException();
        }

        public override void RemoveRow(int index)
        {
            IRow remove = sheet.GetRow(index);
            sheet.RemoveRow(remove);
        }
         
        public void CreateRow(int rowIndex)
        {
            sheet.CreateRow(this.StartRow + rowIndex); 
        }

        public void CreateCell(int rowIndex, int columnIndex, object value, bool useBorder = true)
        {
            IRow row = sheet.GetRow(this.StartRow + rowIndex);
            if (row != null)
            {
                if (value == null)
                {
                    row.CreateCell(columnIndex).SetCellValue("");
                }
                else
                {
                    ICell cell = row.CreateCell(columnIndex);
                    if (useBorder)
                    {
                        //cell.CellStyle.BorderRight = BorderStyle.Medium;
                        //cell.CellStyle.BorderBottom = BorderStyle.Medium;  
                    }

                    Type type = value.GetType();
                    if (type == typeof(string))
                    {
                        cell.SetCellValue(Convert.ToString(value));
                    }
                    else if (type == typeof(DateTime))
                    {
                        cell.SetCellValue(Convert.ToDateTime(value));
                    }
                    else if (type == typeof(Boolean))
                    {
                        cell.SetCellValue(Convert.ToBoolean(value));
                    }
                    else if (type == typeof(Double))
                    {
                        cell.SetCellValue(Convert.ToDouble(value));
                    } 
                } 
            }
        }

        public override void Read()
        {
             GetRows();
        }
    }
}
