using ClosedXML.Excel;

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
        using var workBook = new XLWorkbook(_filePath);
        var rows = workBook.Worksheet(1).RangeUsed().RowsUsed().Skip(1);
        foreach (var row in rows)
        {
            var id = ParceId(row.Cell(1).GetString());
            ids.Add(id);
        }

        return ids;
    }
}