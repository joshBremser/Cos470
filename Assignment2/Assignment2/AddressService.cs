using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Web;
using Newtonsoft.Json;
class AddressServiceMethods
{
    public string url;
    public string query;
    public static List<Address> GetAddresses(string url, string queryString)
    {
        // Using HttpUtility to generate the parameters for the query string handles HTTP escaping the values.
        // Change these:
        var query = HttpUtility.ParseQueryString(queryString);
        var address = url + "?" + query;

        // Disposing HttpClient is not best practice. It's good enough and I'm keeping this simple.
        using (var client = new HttpClient())
        {
            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, address))
            {
                // .Result from an Async function is not best practice. It's good enough and I'm keeping this simple.
                var response = client.SendAsync(request).Result;
                var content = response.Content.ReadAsStringAsync().Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception($"{content}: {response.StatusCode}");
                }
                var model = JsonConvert.DeserializeObject<RootObject>(content);
                return model.features; // the content of the response
            }
        }
    }

    
}
    public class AddressAttributes
    {
        public int ADDRESS_NUMBER { get; set; }
        public string STREETNAME { get; set; }
        public string SUFFIX { get; set; }
        public string MUNICIPALITY { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class Address
    {
        public AddressAttributes attributes { get; set; }
    }

    public class RootObject
    {
        public List<Address> features { get; set; }
    }