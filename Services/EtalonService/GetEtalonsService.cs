using System.Diagnostics;
using GetSiTypeFromArshin.Services.EtalonService.Connection;
using GetSiTypeFromArshin.Services.EtalonService.Excel;

namespace GetSiTypeFromArshin.Services.EtalonService;

public class GetEtalonsService
{
    public async Task<bool> GetEtalons()
    {
        try
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            var etalons = await ApiEtalonConnectionService.GetData();
            CreateEtalonExcelFileService.CreateExcelFile(etalons.response.docs);
            
            
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine("Working time: " + elapsedTime);
        }
        catch (Exception exception)
        {
            Console.WriteLine("Вышрузка завершилась с исключение: "+exception.Message);
            return false;
        }

        return true;
    }
}