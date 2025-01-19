using Microsoft.Playwright;
using DotNetEnv;
using System.Net;
using Newtonsoft.Json.Linq;

namespace NUnitPlaywrightFramework.Libs
{
    public class APIBase
    {
        private Uri baseUrl { get; set; }
        private HttpClient httpClient { get; set; }

        [OneTimeSetUp]
        public void Setup()
        {
            Env.Load();
            baseUrl = new Uri("https://reqres.in");
            httpClient = new HttpClient();
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            httpClient.Dispose();
        }

        public string GetEnvVariable(string variableName)
        {
            string? _val = Environment.GetEnvironmentVariable(variableName);
            return _val ?? string.Empty;
        }

        public async Task<ResponseObjects> GetAsync(string endpoint)
        {
            var _response= await httpClient.GetAsync(new Uri(baseUrl, endpoint));
            var _responseCode = _response.StatusCode;
            var _responseBody = await _response.Content.ReadAsStringAsync();
            var _responseJson = JObject.Parse(_responseBody);
            return new ResponseObjects { ResponseStatusCode = _responseCode, ResponseBody = _responseJson };
        }

        public async Task<ResponseObjects> PostAsync(string endpoint, HttpContent content)
        {
            var _response= await httpClient.PostAsync(new Uri(baseUrl, endpoint), content);
            var _responseCode = _response.StatusCode;
            var _responseBody = await _response.Content.ReadAsStringAsync();
            var _responseJson = JObject.Parse(_responseBody);
            return new ResponseObjects { ResponseStatusCode = _responseCode, ResponseBody = _responseJson };
        }
    }

    public class ResponseObjects
    {
        public HttpStatusCode ResponseStatusCode { get; set; }
        public JObject? ResponseBody { get; set; }
    }
}
