using GetSiTypeFromArshin.Services.SiService;

namespace GetSiTypeFromArshin
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GetTypesService typesService = new GetTypesService();

            var result = await typesService.GetTypes();
            if (result)
                Console.WriteLine("Выгрузка номеров госреестра успешно завершена");
        }
    }
}