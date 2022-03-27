using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseWS.Models;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace BaseWS.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogService : ControllerBase
    {
        public static string Url { get; set; }
        public static HttpClient ApiClient { get; set; }
        
        public CatalogService()
        {
            CatalogService.Url = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("UrlConf")["Url"];
            CatalogService.InitializeClient();
        }

        [HttpGet]
        public async Task<String> Get()
           {
            using (HttpResponseMessage response = await ApiClient.GetAsync(""))
            {
                if (response.IsSuccessStatusCode) {
                    var items = await  response.Content.ReadAsStringAsync();
                    return items;
                }
            };
               return null;
        }

        public static void InitializeClient()
        {
            ApiClient = new HttpClient();
            //ApiClient.BaseAddress = new Uri("https://api.sunrise-sunset.org/json");
            ApiClient.BaseAddress = new Uri(CatalogService.Url);
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }
        


    }
}
