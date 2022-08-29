using GetSiTypeFromArshin.Models;
using GetSiTypeFromArshin.Services.Excel;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GetSiTypeFromArshin
{
    internal class Program
    {
        public static Root root { get; set; }
        public static List<NomerGosReestra> nomera { get; set; } = new List<NomerGosReestra>();
        static void Main(string[] args)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            for (int i = 1; i <= 50; i++)
            {
                Task downloadTask = GetDataAsync(i);
                Task.WaitAll(downloadTask);

                var items = root.result.items;
                foreach (var item in items)
                {
                    nomera.Add(new NomerGosReestra
                    {
                        Num = item.properties.Where(x=>x.title == "Номер в госреестре").Select(x=>x.value?.ToString()).FirstOrDefault(),
                        Name = item.properties.Where(x => x.title == "Наименование СИ").Select(x => x.value?.ToString()).FirstOrDefault(),
                        TypeSi = item.properties.Where(x => x.title == "Обозначение типа СИ").Select(x => x.value?.ToString()).FirstOrDefault()
                    });
                }
            }

            CreateExcelFileService createExcel = new();
            createExcel.CreateExcelFile(nomera);

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Working time: " + elapsedTime);
            //foreach (var nomer in nomera)
            //{
            //    Console.WriteLine($"Номер:{nomer.Num} Обозначение:{nomer.Name} Тип СИ:{nomer.TypeSi}");
            //}

        }

        static async Task GetDataAsync(int pagenum)
        {
            using (var httpClient = new HttpClient())
            {
                var reqUri = $"https://fgis.gost.ru/fundmetrology/api/registry/4/data?pageNumber={pagenum.ToString()}&pageSize=1000&orgID=CURRENT_ORG";
                var responce = await httpClient.GetAsync(reqUri);
                var responceStr = responce.Content.ReadAsStringAsync().Result;
                try
                {
                    root = JsonConvert.DeserializeObject<Root>(responceStr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }


            }
        }
    }
}