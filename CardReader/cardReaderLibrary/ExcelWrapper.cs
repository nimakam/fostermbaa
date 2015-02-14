using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardReaderLibrary
{
    public class ExcelWrapper : IDisposable
    {
        string SheetName;
        string StudentsSheetName = "Students"; 

        public ExcelWrapper(string filePath, uint rowCount = 0, string sheetName = null)
        {
            
            if(sheetName == null)
            {
                sheetName = DateTime.Now.ToLongDateString();
            }

            RowCount = rowCount;
            SheetName = sheetName;

            UInt32Value sheetId;

            if (File.Exists(filePath))
            {
                Package spreadsheetPackage = Package.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

                SpreadsheetDocument = SpreadsheetDocument.Open(spreadsheetPackage);
            }
            else
            { 
                SpreadsheetDocument = SpreadsheetDocument.Create(filePath, SpreadsheetDocumentType.Workbook);
            }            

            // Add a WorkbookPart to the document.
            Workbookpart = SpreadsheetDocument.WorkbookPart;

            if(Workbookpart == null)
            {
                Workbookpart = SpreadsheetDocument.AddWorkbookPart();
                Workbookpart.Workbook = new Workbook();
            }

            Sheets sheets = Workbookpart.Workbook.Descendants<Sheets>().FirstOrDefault();

            if(sheets == null)
            {

                // Add Sheets to the Workbook.
                sheets = SpreadsheetDocument.WorkbookPart.Workbook.
                    AppendChild<Sheets>(new Sheets());
            }


            if (SpreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
            {
                ShareStringPart = SpreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();
            }
            else
            {
                ShareStringPart = SpreadsheetDocument.WorkbookPart.AddNewPart<SharedStringTablePart>();
            }

            Sheet sheet = Workbookpart.Workbook.Descendants<Sheet>().
          Where(s => s.Name == SheetName).FirstOrDefault();

            if(sheet == null)
            {
                // Add a WorksheetPart to the WorkbookPart.
                WorksheetPart = Workbookpart.AddNewPart<WorksheetPart>();
                WorksheetPart.Worksheet = new Worksheet(new SheetData());

                sheetId = Workbookpart.Workbook.Descendants<Sheet>().Any() ? Workbookpart.Workbook.Descendants<Sheet>().Select(s => s.SheetId.Value).Max() + 1 : 1;

                sheet = new Sheet()
                {
                    Id = SpreadsheetDocument.WorkbookPart.
                        GetIdOfPart(WorksheetPart),
                    SheetId = sheetId,
                    Name = SheetName
                };

                sheets.Append(sheet);

                int index = InsertSharedStringItem("Student ID", ShareStringPart);
                Cell cell = InsertCellInWorksheet("A", 1, WorksheetPart);
                cell.CellValue = new CellValue(index.ToString());
                cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                int indexB = InsertSharedStringItem("Time", ShareStringPart);
                Cell cellB = InsertCellInWorksheet("B", 1, WorksheetPart);
                cellB.CellValue = new CellValue(indexB.ToString());
                cellB.DataType = new EnumValue<CellValues>(CellValues.SharedString);

                int indexC = InsertSharedStringItem("Full Name", ShareStringPart);
                Cell cellC = InsertCellInWorksheet("C", 1, WorksheetPart);
                cellC.CellValue = new CellValue(indexC.ToString());
                cellC.DataType = new EnumValue<CellValues>(CellValues.SharedString);
            }
            else
            {
                WorksheetPart = (WorksheetPart)Workbookpart.GetPartById(sheet.Id);
            }                       

            Sheet studentsSheet = Workbookpart.Workbook.Descendants<Sheet>().
        Where(s => s.Name == StudentsSheetName).FirstOrDefault();
            if (studentsSheet != null)
            {
                StudentsWorksheetPart = (WorksheetPart)Workbookpart.GetPartById(studentsSheet.Id);
            }

            Workbookpart.Workbook.Save();
        }

        public class StudentInfo
        {
            public string StudentId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string FullName { get; set; }
            public string Class { get; set; }
            public string Email { get; set; }
            public bool? IsMbaaMember { get; set; }
        }

        public Dictionary<string, StudentInfo> GetIdFullNameLookup()
        {
            var dictionary = new Dictionary<string, StudentInfo>();
            int index = 2;

            if (StudentsWorksheetPart == null)
            {
                return null;
            }

            while (true)            
            {
                var studentInfo = new StudentInfo();

                var lastNameAddress = "A" + index.ToString();
                var lastName = GetCellValue(StudentsWorksheetPart, Workbookpart, lastNameAddress);
                studentInfo.LastName = lastName;

                var firstNameAddress = "B" + index.ToString();
                var firstName = GetCellValue(StudentsWorksheetPart, Workbookpart, firstNameAddress);
                studentInfo.LastName = firstName;

                var netIdAddress = "D" + index.ToString();
                var netId = GetCellValue(StudentsWorksheetPart, Workbookpart, netIdAddress);
                if (!string.IsNullOrEmpty(netId))
                {
                    studentInfo.Email = netId + "@uw.edu";
                }

                var studentIdAddress = "E" + index.ToString();
                var studentId = GetCellValue(StudentsWorksheetPart, Workbookpart, studentIdAddress);
                studentInfo.StudentId = studentId;

                if (string.IsNullOrEmpty(studentId))
                {
                    break;
                }

                var classAddress = "F" + index.ToString();
                var _class = GetCellValue(StudentsWorksheetPart, Workbookpart, classAddress);
                studentInfo.Class = _class;

                var isMbaaMemberAddress = "G" + index.ToString();
                var isMbaaMemberString = GetCellValue(StudentsWorksheetPart, Workbookpart, isMbaaMemberAddress);
                bool? isMbaaMember;
                if (string.IsNullOrEmpty(isMbaaMemberString))
                {
                    isMbaaMember = null;
                }
                else if (isMbaaMemberString.Equals("Yes", StringComparison.OrdinalIgnoreCase))
                {
                    isMbaaMember = true;
                }
                else if (isMbaaMemberString.Equals("No", StringComparison.OrdinalIgnoreCase))
                {
                    isMbaaMember = false;
                }
                else
                {
                    isMbaaMember = null;
                }
                studentInfo.IsMbaaMember = isMbaaMember;

                var fullName = firstName + " " + lastName;
                studentInfo.FullName = fullName;

                if (!dictionary.ContainsKey(studentId))
                {
                    dictionary.Add(studentId, studentInfo);
                }

                index++;
            }

            return dictionary;
        }

        public void AddStudentId(string studentId, string fullName)
        {
            int index = InsertSharedStringItem(studentId, ShareStringPart);
            Cell cell = InsertCellInWorksheet("A", RowCount, WorksheetPart);
            cell.CellValue = new CellValue(index.ToString());
            cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            int indexB = InsertSharedStringItem(DateTime.Now.ToLongTimeString(), ShareStringPart);
            Cell cellB = InsertCellInWorksheet("B", RowCount, WorksheetPart);
            cellB.CellValue = new CellValue(indexB.ToString());
            cellB.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            int indexC = InsertSharedStringItem(fullName, ShareStringPart);
            Cell cellC = InsertCellInWorksheet("C", RowCount, WorksheetPart);
            cellC.CellValue = new CellValue(indexC.ToString());
            cellC.DataType = new EnumValue<CellValues>(CellValues.SharedString);

            RowCount++;

            Workbookpart.Workbook.Save();
        }


        uint RowCount { get; set; }

        SpreadsheetDocument SpreadsheetDocument { get; set; }

        WorksheetPart WorksheetPart { get; set; }

        SharedStringTablePart ShareStringPart { get; set; }

        WorkbookPart Workbookpart { get; set; }
        WorksheetPart StudentsWorksheetPart { get; set; }

        // Given text and a SharedStringTablePart, creates a SharedStringItem with the specified text 
        // and inserts it into the SharedStringTablePart. If the item already exists, returns its index.
        private static int InsertSharedStringItem(string text, SharedStringTablePart shareStringPart)
        {   
            // If the part does not contain a SharedStringTable, create one.
            if (shareStringPart.SharedStringTable == null)
            {
                shareStringPart.SharedStringTable = new SharedStringTable();
            }

            int i = 0;

            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
            {
                if (item.InnerText == text)
                {
                    return i;
                }

                i++;
            }

            // The text does not exist in the part. Create the SharedStringItem and return its index.
            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(text)));
            shareStringPart.SharedStringTable.Save();

            return i;
        }

        // Given a WorkbookPart, inserts a new worksheet.
        private static WorksheetPart InsertWorksheet(WorkbookPart workbookPart)
        {
            // Add a new worksheet part to the workbook.
            WorksheetPart newWorksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            newWorksheetPart.Worksheet = new Worksheet(new SheetData());
            newWorksheetPart.Worksheet.Save();

            Sheets sheets = workbookPart.Workbook.GetFirstChild<Sheets>();
            string relationshipId = workbookPart.GetIdOfPart(newWorksheetPart);

            // Get a unique ID for the new sheet.
            uint sheetId = 1;
            if (sheets.Elements<Sheet>().Count() > 0)
            {
                sheetId = sheets.Elements<Sheet>().Select(s => s.SheetId.Value).Max() + 1;
            }

            string sheetName = "Sheet" + sheetId;

            // Append the new worksheet and associate it with the workbook.
            Sheet sheet = new Sheet() { Id = relationshipId, SheetId = sheetId, Name = sheetName };
            sheets.Append(sheet);
            workbookPart.Workbook.Save();

            return newWorksheetPart;
        }

        // Retrieve the value of a cell, given a file name, sheet name, 
        // and address name.
        public static string GetCellValue(WorksheetPart wsPart, WorkbookPart wbPart,
            string addressName)
        {
            string value = null;

            // Use its Worksheet property to get a reference to the cell 
            // whose address matches the address you supplied.
            Cell theCell = wsPart.Worksheet.Descendants<Cell>().
              Where(c => c.CellReference == addressName).FirstOrDefault();

            // If the cell does not exist, return an empty string.
            if (theCell != null)
            {
                value = theCell.InnerText;

                // If the cell represents an integer number, you are done. 
                // For dates, this code returns the serialized value that 
                // represents the date. The code handles strings and 
                // Booleans individually. For shared strings, the code 
                // looks up the corresponding value in the shared string 
                // table. For Booleans, the code converts the value into 
                // the words TRUE or FALSE.
                if (theCell.DataType != null)
                {
                    switch (theCell.DataType.Value)
                    {
                        case CellValues.SharedString:

                            // For shared strings, look up the value in the
                            // shared strings table.
                            var stringTable =
                                wbPart.GetPartsOfType<SharedStringTablePart>()
                                .FirstOrDefault();

                            // If the shared string table is missing, something 
                            // is wrong. Return the index that is in
                            // the cell. Otherwise, look up the correct text in 
                            // the table.
                            if (stringTable != null)
                            {
                                value =
                                    stringTable.SharedStringTable
                                    .ElementAt(int.Parse(value)).InnerText;
                            }
                            break;

                        case CellValues.Boolean:
                            switch (value)
                            {
                                case "0":
                                    value = "FALSE";
                                    break;
                                default:
                                    value = "TRUE";
                                    break;
                            }
                            break;
                    }
                }

            }
            return value;
        }


        // Given a column name, a row index, and a WorksheetPart, inserts a cell into the worksheet. 
        // If the cell already exists, returns it. 
        private static Cell InsertCellInWorksheet(string columnName, uint rowIndex, WorksheetPart worksheetPart)
        {
            Worksheet worksheet = worksheetPart.Worksheet;
            SheetData sheetData = worksheet.GetFirstChild<SheetData>();
            string cellReference = columnName + rowIndex;

            // If the worksheet does not contain a row with the specified row index, insert one.
            Row row;
            if (sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).Count() != 0)
            {
                row = sheetData.Elements<Row>().Where(r => r.RowIndex == rowIndex).First();
            }
            else
            {
                row = new Row() { RowIndex = rowIndex };
                sheetData.Append(row);
            }

            // If there is not a cell with the specified column name, insert one.  
            if (row.Elements<Cell>().Where(c => c.CellReference.Value == columnName + rowIndex).Count() > 0)
            {
                return row.Elements<Cell>().Where(c => c.CellReference.Value == cellReference).First();
            }
            else
            {
                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
                Cell refCell = null;
                foreach (Cell cell in row.Elements<Cell>())
                {
                    if (string.Compare(cell.CellReference.Value, cellReference, true) > 0)
                    {
                        refCell = cell;
                        break;
                    }
                }

                Cell newCell = new Cell() { CellReference = cellReference };
                row.InsertBefore(newCell, refCell);

                worksheet.Save();
                return newCell;
            }

        }

        public void Dispose()
        {
            SpreadsheetDocument.Close();
        }
    }
}
