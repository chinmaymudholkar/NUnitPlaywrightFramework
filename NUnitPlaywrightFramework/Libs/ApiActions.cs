using Newtonsoft.Json.Linq;
using System.Net;

namespace NUnitPlaywrightFramework.Libs
{
    internal class ApiActions
    {
        private readonly Wrappers wrappers;
        private HttpClient httpClient { get; set; }
        private Uri apiBaseUrl { get; set; }

        internal ApiActions()
        {
            wrappers = new();
            httpClient = new();
            apiBaseUrl = new Uri(new TestBase().GetEnvVariable(EnvironmentVariables.API_BASE_URL));
        }

        internal async Task<ResponseObjects> GetAsync(string endpoint)
        {
            var _response = await httpClient.GetAsync(new Uri(apiBaseUrl, endpoint));
            var _responseCode = _response.StatusCode;
            var _responseBody = await _response.Content.ReadAsStringAsync();
            var _responseJson = JObject.Parse(_responseBody);
            return new ResponseObjects { ResponseStatusCode = _responseCode, ResponseBody = _responseJson };
        }

        internal async Task<ResponseObjects> PostAsync(string endpoint, HttpContent content)
        {
            var _response = await httpClient.PostAsync(new Uri(apiBaseUrl, endpoint), content);
            var _responseCode = _response.StatusCode;
            var _responseBody = await _response.Content.ReadAsStringAsync();
            var _responseJson = JObject.Parse(_responseBody);
            return new ResponseObjects { ResponseStatusCode = _responseCode, ResponseBody = _responseJson };
        }

    }

    internal class ResponseObjects
    {
        public HttpStatusCode ResponseStatusCode { get; set; }
        public JObject? ResponseBody { get; set; }
    }

}
