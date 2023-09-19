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
                    GetTypes();
                    break;
                case "2":
                    GetEtalons();
                    break;
                default:
                    Console.WriteLine("Вы ввели некорректное значение");
                    break;
            }
        }

        static async void GetTypes()
        {
            GetTypesService typesService = new GetTypesService();

            var result = await typesService.GetTypes();
            if (result)
                Console.WriteLine("Выгрузка номеров госреестра успешно завершена");
        }

        static void GetEtalons()
        {
            
        }
    }
}