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
        //private readonly IApiConfig _appConfig; // Puede definirse en la config de la api que haga el request
        private readonly HttpClient _httpClient;

        public ApiCaller(string urlExternalService)
        {
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(urlExternalService)
            };

            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Metodo de ejemplo 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="controller"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetServiceResponseById<T>(string controller, int id)
        {
            var response = await _httpClient.GetAsync(string.Format("/{0}/{1}", controller, id));

            if (!response.IsSuccessStatusCode)
                return default(T);

            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
