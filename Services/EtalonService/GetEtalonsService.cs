using System.Diagnostics;
using GetSiTypeFromArshin.Services.EtalonService.Connection;
using GetSiTypeFromArshin.Services.EtalonService.Excel;

namespace GetSiTypeFromArshin.Services.EtalonService;

public class GetEtalonsService
{
    private readonly List<string> _regNumbers; 
    public GetEtalonsService(List<string> regNumbers)
    {
        _regNumbers = regNumbers;
    }
    
    public async Task<bool> GetEtalons()
    {
        try
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            
            //В GetData нужно передавать найденные ID на сайте аршина.
            List<int> ids = await ApiEtalonConnectionService.GetIds(_regNumbers);
            var etalons = await ApiEtalonConnectionService.GetData(ids);
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