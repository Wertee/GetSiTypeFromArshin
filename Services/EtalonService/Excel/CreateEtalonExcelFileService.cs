using GetSiTypeFromArshin.Models.ApiModels.Etalons.ResponceEtalon;
using OfficeOpenXml;

namespace GetSiTypeFromArshin.Services.EtalonService.Excel;

public class CreateEtalonExcelFileService
{
    public static void CreateExcelFile(List<Result> etalons)
    {
        string unloadDisk = "";
        var directory = new DirectoryInfo($@"{unloadDisk}:\");
        while (!directory.Exists)
        {
            Console.WriteLine("Введите букву диска, куда выгружаем файл с эталонами");
            unloadDisk = Console.ReadLine();
            directory = new DirectoryInfo($@"{unloadDisk}:\");
        }
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage($@"{unloadDisk}:\Etalons {DateTime.Now:dd_MM_yyyy_HH_mm_ss}.xlsx"))
        {
            Console.WriteLine("Создаем excel файл");
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
                    var lastResultDate = etalonDataToExcelModel.cresults.OrderByDescending(x => DateOnly.Parse(x.verification_date))
                        .Select(x => x).First();
                    
                    sheet.Cells[currentRow, 1].Value = etalonDataToExcelModel.number;
                    sheet.Cells[currentRow, 2].Value = etalonDataToExcelModel.factory_num;
                    sheet.Cells[currentRow, 3].Value = DateTime
                        .Parse(lastResultDate.verification_date).ToShortDateString();
                    sheet.Cells[currentRow, 4].Value = DateTime
                        .Parse(lastResultDate.valid_date).ToShortDateString();
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

    public static void CreateExcelFileNotFound(List<string> etalons)
    {
        string unloadDisk = "";
        var directory = new DirectoryInfo($@"{unloadDisk}:\");
        
        if(etalons.Count ==0)
            return;
        
        while (!directory.Exists)
        {
            Console.WriteLine("Введите букву диска, куда выгружаем файл с ненайденными эталонами");
            unloadDisk = Console.ReadLine();
            directory = new DirectoryInfo($@"{unloadDisk}:\");
        }
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage($@"{unloadDisk}:\Etalons not found {DateTime.Now:dd_MM_yyyy_HH_mm_ss}.xlsx"))
        {
            Console.WriteLine("Создаем excel файл");
            var sheet = package.Workbook.Worksheets.Add("Etalons");
            sheet.Cells[1, 1].Value = "Регистрационный номер эталона";

            int currentRow = 2;
            foreach (var etalon in etalons)
            {
                try
                {
                    sheet.Cells[currentRow, 1].Value = etalon;
                }
                catch (Exception e)
                {
                    sheet.Cells[currentRow, 1].Value = etalon;
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