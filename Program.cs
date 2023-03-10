using GetSiTypeFromArshin.Models;
using GetSiTypeFromArshin.Services.Data;
using GetSiTypeFromArshin.Services.Excel;
using System.Diagnostics;

namespace GetSiTypeFromArshin
{
    internal class Program
    {
        public static List<DataToExcel> numbers { get; set; } = new();
        static async Task Main(string[] args)
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
                    var n = item.properties.Where(x => x.title == "Номер в госреестре")
                        .Select(x => x.value?.ToString())
                        .FirstOrDefault();
                    if (n.Equals("3-40"))
                        Console.WriteLine("1");
                    numbers.Add(new DataToExcel()
                    {
                        Number = item.properties.Where(x => x.title == "Номер в госреестре").Select(x => x.value?.ToString()).FirstOrDefault(),
                        Name = item.properties.Where(x => x.title == "Наименование СИ").Select(x => x.value?.ToString()).FirstOrDefault(),
                        TypeSi = item.properties.Where(x => x.title == "Обозначение типа СИ").Select(x => x.value?.ToString()).FirstOrDefault()?.Replace("[", "").Replace("]", ""),
                        Manufacturer = item.properties.Where(x => x.title == "Изготовитель").Select(x => x.value?.ToString()).FirstOrDefault(),
                        CheckPeriod = item.properties.Where(x => x.title == "МПИ").Select(x => x.value?.ToString()).FirstOrDefault()
                    });
                }
            }

            CreateExcelFileService createExcel = new();
            createExcel.CreateExcelFile(numbers);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Working time: " + elapsedTime);

        }
    }
}