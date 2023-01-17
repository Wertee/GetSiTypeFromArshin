using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GetSiTypeFromArshin.Models;

namespace GetSiTypeFromArshin.Services.Excel
{
    public class CreateExcelFileService
    {
        public void CreateExcelFile(List<DataToExcel> numbers)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(@"D:\Test2.xlsx"))
            {
                Console.WriteLine("Creating Excel file");
                var sheet = package.Workbook.Worksheets.Add("Nomera");

                sheet.Cells[1, 1].Value = "Номер гос рееста";
                sheet.Cells[1, 2].Value = "Наименование типа СИ";
                sheet.Cells[1, 3].Value = "Тип СИ";

                for (int i = 2; i < numbers.Count; i++)
                {
                    sheet.Cells[i, 1].Value = numbers[i - 2].Num;
                    sheet.Cells[i, 2].Value = numbers[i - 2].Name;
                    sheet.Cells[i, 3].Value = numbers[i - 2].TypeSi;
                }


                package.Save();
            }
        }
    }
}
