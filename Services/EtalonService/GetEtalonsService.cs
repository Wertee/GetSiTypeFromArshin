using System.Diagnostics;
using GetSiTypeFromArshin.Services.EtalonService.Connection;
using GetSiTypeFromArshin.Services.EtalonService.Excel;

namespace GetSiTypeFromArshin.Services.EtalonService;

public class GetEtalonsService
{
    private readonly List<int> _ids; 
    public GetEtalonsService(List<int> ids)
    {
        _ids = ids;
    }
    
    public async Task<bool> GetEtalons()
    {
        try
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            var etalons = await ApiEtalonConnectionService.GetData(_ids);
            CreateEtalonExcelFileService.CreateExcelFile(etalons);
            
            
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Время работы: " + elapsedTime);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Выгрузка завершилась с исключение: "+exception.Message);
            return false;
        }

        return true;
    }
}