using OfficeOpenXml;

namespace GetSiTypeFromArshin.Services.EtalonService.Excel;

public class GetEtalonExcelFileService
{
    private readonly string _filePath;
    
    public GetEtalonExcelFileService(string filePath)
    {
        _filePath = filePath;
    }

    public int ParceId(string regNumber)
    {
        var lastIndex = regNumber.LastIndexOf('.');
        var parsed = string.Join("",regNumber.TakeLast(regNumber.Length - lastIndex - 1)).TrimStart('0');
        var id = int.Parse(parsed);
        return id;
    }
    
    public List<int> GetEtalonsId()
    {
        List<int> ids = new();
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        FileInfo fileInfo = new FileInfo(_filePath);
        ExcelPackage package = new ExcelPackage(fileInfo);
        var worksheet = package.Workbook.Worksheets.FirstOrDefault();
        var rows = worksheet.Dimension.Rows;
        for (int i = 2;i <= rows;i++)
        {
            var id = ParceId(worksheet.Cells[i,1].Value.ToString());
            ids.Add(id);
        }

        return ids;
    }
}