using Newtonsoft.Json.Linq;
using RestCountriesApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;

namespace RestCountriesApp.Utilities
{
    /// <summary>
    /// Access logic to Database and RestAPI services
    /// </summary>
    public static class DataAccess
    {
        private static readonly string requestURI = @"https://restcountries.eu/rest/v2/all";

        /// <summary>
        /// Gets List of JSON objects from the website
        /// Used Newtonsoft's JObject and JArray to keep objects
        /// </summary>
        /// <returns>A List of JObjects</returns>
        public static List<JObject> GetAllCountries()
        {
            using HttpClient httpClient = new HttpClient();
            HttpResponseMessage responseMessage = httpClient.GetAsync(requestURI).Result;

            string responseBody = responseMessage.Content.ReadAsStringAsync().Result;

            var jArray = JArray.Parse(responseBody);

            List<JObject> jObjects = jArray.ToObject<List<JObject>>();

            return jObjects;
        }

        public static ObservableCollection<CountryDAO> GetCountriesFromDB()
        {
            if(!DBManager.CheckCountriesExist())
            {
                DBManager.InsertCountries(GetAllCountries());
            }
            return DBManager.GetCountries();
        }

        /// <summary>
        /// Downloads svg file and puts the data into MemoryStream
        /// </summary>
        /// <param name="url">Url path of svg file</param>
        /// <returns>A MemoryStream instance</returns>
        public static MemoryStream DownloadSvgFile(string url)
        {
            using HttpClient client = new HttpClient();

            using Stream stream = client.GetStreamAsync(url).Result;

            MemoryStream ms = new MemoryStream();

            stream.CopyTo(ms);

            return ms;
        }
    }
}
