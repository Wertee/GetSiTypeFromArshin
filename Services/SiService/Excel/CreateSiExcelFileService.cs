using System.ComponentModel;
using GetSiTypeFromArshin.Models;
using GetSiTypeFromArshin.Models.ApiModels.SiTypes.ExcelTypes;
using OfficeOpenXml;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace GetSiTypeFromArshin.Services.SiService.Excel
{
    public class CreateSiExcelFileService
    {
        public void CreateExcelFile(List<TypesDataToExcelModel> numbers)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(@"F:\Gosreestr.xlsx"))
            {
                Console.WriteLine("Creating Excel file");
                var sheet = package.Workbook.Worksheets.Add("Nomera");

                sheet.Cells[1, 1].Value = "Номер гос рееста";
                sheet.Cells[1, 2].Value = "Наименование типа СИ";
                sheet.Cells[1, 3].Value = "Тип СИ";
                sheet.Cells[1, 4].Value = "Производитель";
                sheet.Cells[1, 5].Value = "МПИ";

                for (int i = 0; i < numbers.Count; i++)
                {
                    sheet.Cells[i + 2, 1].Value = numbers[i].Number;
                    sheet.Cells[i + 2, 2].Value = numbers[i].Name;
                    sheet.Cells[i + 2, 3].Value = numbers[i].TypeSi;
                    sheet.Cells[i + 2, 4].Value = numbers[i].Manufacturer;
                    sheet.Cells[i + 2, 5].Value = numbers[i].CheckPeriod;
                }


                package.Save();
            }
        }
    }
}
