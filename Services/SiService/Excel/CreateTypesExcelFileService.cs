using System.ComponentModel;
using GetSiTypeFromArshin.Models;
using GetSiTypeFromArshin.Models.ApiModels.SiTypes.ExcelTypes;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace GetSiTypeFromArshin.Services.SiService.Excel
{
    public class CreateTypesExcelFileService
    {
        public void CreateExcelFile(List<TypesDataToExcelModel> numbers)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(@"D:\Gosreestr.xlsx"))
            {
                Console.WriteLine("Creating Excel file");
                var sheet = package.Workbook.Worksheets.Add("Nomera");

                sheet.Cells[1, 1].Value = "Номер гос рееста";
                sheet.Cells[1, 2].Value = "Наименование типа СИ";
                sheet.Cells[1, 3].Value = "Тип СИ";
                sheet.Cells[1, 4].Value = "Производитель";
                sheet.Cells[1, 5].Value = "МПИ";
                
                int currentRow = 2;
                foreach (var number in numbers)
                {
                    sheet.Cells[currentRow, 1].Value = number.Number;
                    sheet.Cells[currentRow, 2].Value = number.Name;
                    sheet.Cells[currentRow, 3].Value = number.TypeSi;
                    sheet.Cells[currentRow, 4].Value = number.Manufacturer;
                    sheet.Cells[currentRow, 5].Value = number.CheckPeriod;

                    currentRow++;
                }


                package.Save();
            }
        }
    }
}
