using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetSiTypeFromArshin.Services.Excel
{
    public class CreateExcelFileService
    {
        public void CreateExcelFile(List<NomerGosReestra> nomera)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(@"E:\Test.xlsx"))
            {
                var sheet = package.Workbook.Worksheets.Add("Nomera");

                sheet.Cells[1, 1].Value = "Номер гос рееста";
                sheet.Cells[1, 2].Value = "Наименование типа СИ";
                sheet.Cells[1, 3].Value = "Тип СИ";

                for (int i = 2; i < nomera.Count; i++)
                {
                    sheet.Cells[i, 1].Value = nomera[i - 2].Num;
                    sheet.Cells[i, 2].Value = nomera[i - 2].Name;
                    sheet.Cells[i, 3].Value = nomera[i - 2].TypeSi;
                }


                package.Save();
            }
        }
    }
}
