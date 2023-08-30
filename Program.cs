using GetSiTypeFromArshin.Models;
using System.Diagnostics;
using GetSiTypeFromArshin.Services.SiService;
using GetSiTypeFromArshin.Services.SiService.Data;
using GetSiTypeFromArshin.Services.SiService.Excel;

namespace GetSiTypeFromArshin
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            GetTypesService typesService = new GetTypesService();
            await typesService.GetTypes();
        }
    }
}