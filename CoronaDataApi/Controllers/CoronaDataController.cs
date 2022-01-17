using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoronaDataApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoronaDataController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<Countryy>> getDataAsync(string country)
        {
            if (string.IsNullOrEmpty(country))
            {
                return NotFound();
            }
            if (country.Length>500)
            {
                return NotFound();
            }
            var httpClient = new HttpClient();
            var urlAddress = "https://api.covid19api.com/summary";

            var response = await httpClient.GetAsync(urlAddress);
            var apiResponse = await response.Content.ReadAsStringAsync();
            var convertedResponse = JsonConvert.DeserializeObject<Rootobject>(apiResponse);
            var foundCountry = convertedResponse
                .Countries
                .FirstOrDefault(c =>
                    c.Country.Equals(country, StringComparison.InvariantCultureIgnoreCase) 
                    || c.CountryCode.Equals(country, StringComparison.InvariantCultureIgnoreCase)
                );
            if (foundCountry is null)
            {
                return NotFound();
            }
            return foundCountry;

            //JObject res = JObject.Parse(apiResponse);
            //JArray data = (JArray)res["Countries"];
            //var queriedData = data.Where(c => (string)c["Country"] == country).ToList()[0];
            //Console.WriteLine(queriedData.ToList());
            //return convertedResponse;
        }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class CoronaDynamicDataController : ControllerBase
    {
        [HttpGet]
        public async Task<object> getDataDynamicAsync(string country)
        {
            if (string.IsNullOrEmpty(country))
            {
                return NotFound();
            }
            if (country.Length > 500)
            {
                return NotFound();
            }
            var httpClient = new HttpClient();
            var urlAddress = "https://api.covid19api.com/summary";

            var response = await httpClient.GetAsync(urlAddress);
            var apiResponse = await response.Content.ReadAsStringAsync();
            dynamic convertedResponse = JsonConvert.DeserializeObject<ExpandoObject>(apiResponse);
            var foundCountry = convertedResponse
                .Countries
                .FirstOrDefault();
            if (foundCountry is null)
            {
                return NotFound();
            }
            return foundCountry;

            //JObject res = JObject.Parse(apiResponse);
            //JArray data = (JArray)res["Countries"];
            //var queriedData = data.Where(c => (string)c["Country"] == country).ToList()[0];
            //Console.WriteLine(queriedData.ToList());
            //return convertedResponse;
        }
    }


    public class Rootobject
    {
        public string ID { get; set; }
        public string Message { get; set; }
        public Global Global { get; set; }
        public Countryy[] Countries { get; set; }
        public DateTime Date { get; set; }
    }

    public class Global
    {
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public DateTime Date { get; set; }
    }

    public class Countryy
    {
        public string ID { get; set; }
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string Slug { get; set; }
        public int NewConfirmed { get; set; }
        public int TotalConfirmed { get; set; }
        public int NewDeaths { get; set; }
        public int TotalDeaths { get; set; }
        public int NewRecovered { get; set; }
        public int TotalRecovered { get; set; }
        public DateTime Date { get; set; }
        public Premium Premium { get; set; }
    }

    public class Premium
    {
    }

}


