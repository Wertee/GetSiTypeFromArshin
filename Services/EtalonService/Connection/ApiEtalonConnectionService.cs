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

    public static async Task<Root?> GetData()
    {
        try
        {
            var response = await GetResponseFromApi(
                "https://fgis.gost.ru/fundmetrology/cm/icdb/mieta/select?fq=mitype_num:*29219%5C-05*&fq=factory_num:*060501*&q=*&fl=rmieta_id,number,organization,mitype_num,mitype,minotation,modification,factory_num,year,npenumber,rankcode,verification_date,applicability&sort=verification_date+desc&rows=20&start=0");
            var root = JsonConvert.DeserializeObject<Root?>(response);
            return root;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }
}