using OfficeOpenXml;

namespace GetSiTypeFromArshin.Services.EtalonService.Excel;

public class GetEtalonExcelFileService
{
    private readonly string _filePath;
    
    public GetEtalonExcelFileService(string filePath)
    {
        _filePath = filePath;
    }
    
    public List<string> GetEtalonsRegNumbers()
    {
        List<string> regNumbers = new();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        FileInfo fileInfo = new FileInfo(_filePath);
        ExcelPackage package = new ExcelPackage(fileInfo);
        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        var rows = worksheet.Dimension.Rows;
        for (int i = 2;i <= rows;i++)
        {
            
            regNumbers.Add(worksheet.Cells[i,1].Value.ToString().Trim());
        }

        return regNumbers;
    }
}