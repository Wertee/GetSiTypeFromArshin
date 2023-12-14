using GetSiTypeFromArshin.Services.EtalonService;
using GetSiTypeFromArshin.Services.EtalonService.Excel;
using GetSiTypeFromArshin.Services.SiService;

namespace GetSiTypeFromArshin
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Введите тип получаемых данных");
            Console.WriteLine("1 - Номера госреестра");
            Console.WriteLine("2 - Эталоны");
            var data = Console.ReadLine();

            switch (data)
            {
                case "1":
                    await GetTypes();
                    break;
                case "2":
                    await GetEtalons();
                    break;
                default:
                    Console.WriteLine("Вы ввели некорректное значение");
                    break;
            }

            Console.WriteLine("Для закрытия программы нажмити любую клавишу");
            Console.ReadKey();
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
            if(result)
                Console.WriteLine("Выгрузка успешно завершена");
            
        }
    }
}