using GetSiTypeFromArshin.Models.ApiModels.SiTypes;
using Newtonsoft.Json;

namespace GetSiTypeFromArshin.Services.SiService.Data
{
    public class DataService
    {
        private static async Task<string> GetResponseFromApi(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                var reqUri = uri;
                var responce = await httpClient.GetAsync(reqUri);
                var responceStr = responce.Content.ReadAsStringAsync().Result;
                return responceStr;
            }
        }

        public static async Task<int> GetCountOfPages()
        {
            try
            {
                var responce = await GetResponseFromApi($"https://fgis.gost.ru/fundmetrology/api/registry/4/data");
                var root = JsonConvert.DeserializeObject<Root>(responce);
                return (root.result.totalCount / 1000) + 1;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return 0;
        }

        public static async Task<Root?> GetData(int pageNum)
        {
            try
            {
                var responce =
                    await GetResponseFromApi(
                        $"https://fgis.gost.ru/fundmetrology/api/registry/4/data?pageNumber={pageNum}&pageSize=1000&orgID=CURRENT_ORG");
                var root = JsonConvert.DeserializeObject<Root>(responce);
                return root;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            return null;
        }

    }
}
