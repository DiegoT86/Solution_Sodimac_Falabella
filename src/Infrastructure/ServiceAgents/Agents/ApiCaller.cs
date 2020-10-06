using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Agents
{
    /// <summary>
    /// Clase para manejo de servicios externos
    /// </summary>
    public class ApiCaller : IApiCaller
    {
        // Ref.: https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        private static HttpClient _httpClient = new HttpClient();

        public ApiCaller()
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Metodo de ejemplo 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetServiceResponseById<T>(string baseAddress, string controller, int id)
        {
            var reqUri = string.Format("{0}/{1}/{2}", baseAddress, controller, id);
            var response = await _httpClient.GetAsync(reqUri);

            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
