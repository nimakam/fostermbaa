using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CardReaderLibrary;
using System.IO;
using System.Diagnostics;

namespace CardReader.UnitTests
{
    [TestClass]
    public class ExcelWrapperTests
    {
        [TestMethod]
        public void ExcelWrapperCreateTest()
        {
            var tempFilePath = Path.GetTempFileName() + ".xlsx";
            var sheetName = DateTime.Now.ToLongDateString();
            uint rowCount = 1;
            File.Delete(tempFilePath);
            using (var excelWrapper = new ExcelWrapper(tempFilePath, rowCount, sheetName))
            {
                rowCount++;
                Assert.IsTrue(File.Exists(tempFilePath));
                Console.WriteLine(tempFilePath);

                excelWrapper.AddStudentId("12345678", "Name LastName");
                rowCount++;
            }

            using (var excelWrapper = new ExcelWrapper(tempFilePath, rowCount, sheetName))
            {
                Assert.IsTrue(File.Exists(tempFilePath));
                Console.WriteLine(tempFilePath);

                excelWrapper.AddStudentId("87654321", "FN LN");
            }

            //Process.Start(tempFilePath);

        }
    }
}
