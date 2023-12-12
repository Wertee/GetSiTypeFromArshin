using GetSiTypeFromArshin.Models.ApiModels.Etalons.ResponceEtalon;
using OfficeOpenXml;

namespace GetSiTypeFromArshin.Services.EtalonService.Excel;

public class CreateEtalonExcelFileService
{
    public static void CreateExcelFile(List<Result> etalons)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(@"D:\Etalons.xlsx"))
        {
            var sheet = package.Workbook.Worksheets.Add("Etalons");
            sheet.Cells[1, 1].Value = "Регистрационный номер эталона";
            sheet.Cells[1, 2].Value = "Заводской номер";
            sheet.Cells[1, 3].Value = "Дата поверки";
            sheet.Cells[1, 4].Value = "Дата годен до";

            int currentRow = 2;
            foreach (var etalonDataToExcelModel in etalons)
            {
                try
                {
                    sheet.Cells[currentRow, 1].Value = etalonDataToExcelModel.number;
                    sheet.Cells[currentRow, 2].Value = etalonDataToExcelModel.factory_num;
                    sheet.Cells[currentRow, 3].Value = DateTime
                        .Parse(etalonDataToExcelModel.cresults.OrderBy(x => x.valid_date)
                            .Select(x => x.verification_date).First()).ToShortDateString();
                    ;
                    sheet.Cells[currentRow, 4].Value = DateTime
                        .Parse(etalonDataToExcelModel.cresults.OrderBy(x => x.valid_date).Select(x => x.valid_date)
                            .First()).ToShortDateString();
                }
                catch (Exception e)
                {
                    sheet.Cells[currentRow, 1].Value = etalonDataToExcelModel.number;
                }
                finally
                {
                    currentRow++;
                }
            }
            package.Save();
        }
    }
}