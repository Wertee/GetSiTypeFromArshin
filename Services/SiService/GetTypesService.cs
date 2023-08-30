using System.Diagnostics;
using GetSiTypeFromArshin.Models;
using GetSiTypeFromArshin.Services.SiService.Data;
using GetSiTypeFromArshin.Services.SiService.Excel;

namespace GetSiTypeFromArshin.Services.SiService;

public class GetTypesService
{
    public static List<DataToExcel> numbers { get; set; } = new();

    public async Task<bool> GetTypes()
    {
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();

        var totalPages = await DataService.GetCountOfPages();

        for (int i = 1; i <= totalPages; i++)
        {
            Console.WriteLine($"Iteration {i} of {totalPages}");
            var root = await DataService.GetData(i);
            if (root == null)
            {
                Console.WriteLine("Root is null");
                break;
            }

            var items = root.result.items;

            foreach (var item in items)
            {
                numbers.Add(new DataToExcel()
                {
                    Number = item.properties.Where(x => x.title == "Номер в госреестре")
                        .Select(x => x.value?.ToString()).FirstOrDefault(),
                    Name = item.properties.Where(x => x.title == "Наименование СИ").Select(x => x.value?.ToString())
                        .FirstOrDefault(),
                    TypeSi = item.properties.Where(x => x.title == "Обозначение типа СИ")
                        .Select(x => x.value?.ToString()).FirstOrDefault()?.Replace("[", "").Replace("]", ""),
                    Manufacturer = item.properties.Where(x => x.title == "Изготовитель")
                        .Select(x => x.value?.ToString()).FirstOrDefault(),
                    CheckPeriod = item.properties.Where(x => x.title == "МПИ").Select(x => x.value?.ToString())
                        .FirstOrDefault()
                });
            }
        }

        if (numbers.Count == 0)
            return false;

        try
        {
            CreateExcelFileService createExcel = new();
            createExcel.CreateExcelFile(numbers);
        }
        catch (Exception exception)
        {
            return false;
        }
        

        stopWatch.Stop();
        TimeSpan ts = stopWatch.Elapsed;
        string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
        Console.WriteLine("Working time: " + elapsedTime);

        return true;
    }
}