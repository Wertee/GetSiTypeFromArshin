using GetSiTypeFromArshin.Models.ApiModels.Etalons.ResponceEtalon;
using GetSiTypeFromArshin.Services.EtalonService.Excel;
using Newtonsoft.Json;

namespace GetSiTypeFromArshin.Services.EtalonService.Connection;

public class ApiEtalonConnectionService
{
    private static async Task<string> GetResponseFromApi(string uri)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            try
            {
                var response = await httpClient.GetAsync(uri);
                var content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "null";
            }
            
        }
    }

    public static async Task<List<Result?>> GetEtalons(List<string> regNumbers)
    {
        List<int> ids = await ApiEtalonConnectionService.GetIds(regNumbers);
        var etalons = await ApiEtalonConnectionService.GetData(ids);
        return etalons;
    }
    
    private static async Task<List<Result?>> GetData(List<int> ids)
    {
        List<Result> rootList = new();
        string response = "";
        try
        {
            for (int i = 0; i < ids.Count; i++)
            {
                Thread.Sleep(500);
                try
                {
                    response = await GetResponseFromApi($"https://fgis.gost.ru/fundmetrology/eapi/mieta/{ids[i]}");
                    var root = JsonConvert.DeserializeObject<Root?>(response);
                    Console.WriteLine($"№ {i+1} из {ids.Count}");
                    if(root.result == null)
                        rootList.Add(new Result(){number = ids[i].ToString()});
                    else
                        rootList.Add(root.result);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Не найденный id {i}");
                    Console.WriteLine(e);
                }
                
            }
            return rootList;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(response);
            return null;
        }
    }

    private static async Task<List<int>> GetIds(List<string> regNumbers)
    {
        List<int> ids = new();
        List<string> notFoundEtalons = new();
        string response = "";
        try
        {
            for (int i = 0; i < regNumbers.Count; i++)
            {
                Thread.Sleep(500);
                try
                {
                    response = await GetResponseFromApi($"https://fgis.gost.ru/fundmetrology/eapi/mieta?search=*{regNumbers[i]}*");
                    var root = JsonConvert.DeserializeObject<Models.ApiModels.Etalons.ResponceEtalonId.Root?>(response);
                    Console.WriteLine($"Ищем эталон по рег.№ {i+1} из {regNumbers.Count}");
                     if(root.result.count == 0)
                         notFoundEtalons.Add(regNumbers[i]);
                     else
                         ids.Add(int.Parse(root.result.items[0].rmieta_id));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                
            }
            CreateEtalonExcelFileService.CreateExcelFileNotFound(notFoundEtalons);
            return ids;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            Console.WriteLine(response);
            return null;
        }
    }
}