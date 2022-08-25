using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;

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
            //Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(JsonResStr.GetJJJ());
            Task downloadTask = GetDataAsync();
            Task.WaitAll(downloadTask);

            var items = root.result.items;
            foreach (var item in items)
            {
                nomera.Add(new NomerGosReestra
                {
                    Num = item.properties[8].value.ToString(),
                    Name = item.properties[6].value.ToString(),
                    TypeSi = item.properties[15].value.ToString()
                });
            }
            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            Console.WriteLine("Working time: "+ elapsedTime);
            foreach (var nomer in nomera)
            {
                Console.WriteLine($"Номер:{nomer.Num} Обозначение:{nomer.Name} Тип СИ:{nomer.TypeSi}");
            }

        }

        static async Task GetDataAsync()
        {
            using (var httpClient = new HttpClient())
            {
                var responce = await httpClient.GetAsync("https://fgis.gost.ru/fundmetrology/api/registry/4/data?pageNumber=1&pageSize=100&orgID=CURRENT_ORG");
                var responceStr = responce.Content.ReadAsStringAsync().Result;
                root = JsonConvert.DeserializeObject<Root>(responceStr);

            }
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Item
    {
        public object values { get; set; }
        public List<Property> properties { get; set; }
        public string id { get; set; }
        public string alfrescoId { get; set; }
        public object nodeRef { get; set; }
        public string type { get; set; }
        public object permissions { get; set; }
    }

    public class Property
    {
        public string name { get; set; }
        public string type { get; set; }
        public bool multiple { get; set; }
        public string title { get; set; }
        public string constraint { get; set; }
        public object value { get; set; }
        public string longValue { get; set; }
        public object link { get; set; }
        public string mime { get; set; }
    }

    public class Result
    {
        public int totalCount { get; set; }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public List<Item> items { get; set; }
    }

    public class Root
    {
        public int status { get; set; }
        public Result result { get; set; }
        public object message { get; set; }
        public object trace { get; set; }
    }
}