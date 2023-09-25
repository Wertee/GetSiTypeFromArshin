using GetSiTypeFromArshin.Models.ApiModels.Etalons.ResponceEtalon;
using OfficeOpenXml;

namespace GetSiTypeFromArshin.Services.EtalonService.Excel;

public class CreateEtalonExcelFileService
{
    public static void CreateExcelFile(List<Doc> etalons)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(@"D:\Etalons.xlsx"))
        {
            var sheet = package.Workbook.Worksheets.Add("Etalons");
            sheet.Cells[1, 1].Value = "Регистрационный номер эталона";
            sheet.Cells[1, 2].Value = "Наименование эталона";
            sheet.Cells[1, 3].Value = "Модификация эталона";
            sheet.Cells[1, 4].Value = "Дата поверки";

            int currentRow = 2;
            foreach (var etalonDataToExcelModel in etalons)
            {
                sheet.Cells[currentRow, 1].Value = etalonDataToExcelModel.number;
                sheet.Cells[currentRow, 2].Value = etalonDataToExcelModel.mitype;
                sheet.Cells[currentRow, 3].Value = etalonDataToExcelModel.modification;
                sheet.Cells[currentRow, 4].Value = etalonDataToExcelModel.verification_date.ToShortDateString();
                currentRow++;
            }
            
            package.Save();
        }
    }
}