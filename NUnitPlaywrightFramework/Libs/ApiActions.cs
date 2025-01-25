using Newtonsoft.Json.Linq;

namespace NUnitPlaywrightFramework.Libs
{
    internal class ApiActions : ApiBase
    {
        internal ApiActions()
        {
            ApiBaseSetup().Wait();
        }

        public async Task<ResponseObjects> Get(string endpoint)
        {
            var response = await apiContext.GetAsync(endpoint);
            var jsonResponse = await response.JsonAsync();
            return new ResponseObjects
            {
                ResponseStatusCode = response.Status,
                ResponseBody = jsonResponse.HasValue ? JObject.Parse(jsonResponse.Value.ToString()) : null
            };
        }
    }

    internal class ResponseObjects
    {
        public int ResponseStatusCode { get; set; }
        public JObject? ResponseBody { get; set; }
    }

}
