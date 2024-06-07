using System.Net;
using GetSiTypeFromArshin.Services.EtalonService;
using GetSiTypeFromArshin.Services.EtalonService.Excel;
using GetSiTypeFromArshin.Services.SiService;
using GetSiTypeFromArshin.Services.SiService.Connection;
using Newtonsoft.Json;

namespace GetSiTypeFromArshin
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите тип получаемых данных");
            Console.WriteLine("1 - Номера госреестра");
            Console.WriteLine("2 - Эталоны");
            Console.WriteLine("3 - Проба выгрузки файла");
            var data = Console.ReadLine();

            switch (data)
            {
                case "1":
                    await GetTypes();
                    break;
                case "2":
                    await GetEtalons();
                    break;
                case "3":
                    await GetFile();
                    break;
                default:
                    Console.WriteLine("Вы ввели некорректное значение");
                    break;
            }

            Console.WriteLine("Для закрытия программы нажмите любую клавишу");
            Console.ReadKey();
        }

        private static async Task GetFile()
        {
            var totalPages = await ApiTypesConnectionService.GetCountOfPages();
            for (int i = 1; i <= totalPages; i++)
            {
                var root = await ApiTypesConnectionService.GetData(i);
                if (root == null)
                {
                    Console.WriteLine("Ошибка получения данных");
                    return;
                }

                var items = root.result.items;

                Console.Clear();
                Console.Write("Укажите путь куда сохраняем файлы:");
                string? path = Console.ReadLine();
                while (string.IsNullOrWhiteSpace(path))
                {
                    Console.Write("Укажите путь куда сохраняем файлы:");
                    path = Console.ReadLine();
                }

                for (int j = 0; j < items.Count; j++)
                {
                    Console.WriteLine("Скачивается файл №" + ++j + " из " + items.Count);


                    using (var client = new HttpClient())
                    {
                        string? filename = items[j].properties.Where(x => x.title == "Методики поверки")
                            .Select(x => x.value).FirstOrDefault().ToString();
                        string? address = $"https://fgis.gost.ru/fundmetrology/" +
                                          items[j].properties.Where(x => x.title == "Методики поверки")
                                              .Select(x => x.link).FirstOrDefault();
                        using (var s = client.GetStreamAsync(address))
                        {
                            using (var fs = new FileStream(path + filename, FileMode.OpenOrCreate))
                            {
                                s.Result.CopyTo(fs);
                            }
                        }
                    }
                }
            }
        }

        static async Task GetTypes()
        {
            GetTypesService typesService = new GetTypesService();

            var result = await typesService.GetTypes();
            if (result)
                Console.WriteLine("Выгрузка номеров госреестра успешно завершена");
        }

        static async Task GetEtalons()
        {
            Console.Clear();
            Console.Write("Укажите путь до файла:");
            string? path = Console.ReadLine();
            while (string.IsNullOrWhiteSpace(path) || !File.Exists(path))
            {
                Console.Write("Укажите путь до файла:");
                path = Console.ReadLine();
            }


            var etExService = new GetEtalonExcelFileService(path);
            var regNumbers = etExService.GetEtalonsRegNumbers();
            GetEtalonsService etalonsService = new GetEtalonsService(regNumbers);
            var result = await etalonsService.GetEtalons();
            if (result)
                Console.WriteLine("Выгрузка успешно завершена");
        }
    }
}