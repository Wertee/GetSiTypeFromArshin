using GetSiTypeFromArshin.Models.ApiModels.Etalons.ResponceEtalon;
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
    
    public static async Task<List<Result?>> GetData(List<int> ids)
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
                    Console.WriteLine($"Iteration {i}");
                    if(root.result == null)
                        rootList.Add(new Result(){number = ids[i].ToString()});
                    else
                        rootList.Add(root.result);
                }
                catch (Exception e)
                {
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
}