using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Flurl;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SwedbankSharp
{
    public class SwedbankRequester
    {
        private readonly string _dsid;
        private readonly HttpClient _client;
        private readonly CookieContainer _cookieContainer = new CookieContainer();

        private readonly string _baseUri = "https://auth.api.swedbank.se/TDE_DAP_Portal_REST_WEB/api/v5/";

        public SwedbankRequester(string authKey, string dsid, string userAgent)
        {
            _dsid = dsid;
            HttpClient client = new HttpClient(new HttpClientHandler()
            {
                CookieContainer = _cookieContainer,
                AllowAutoRedirect = true,
                UseCookies = true,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });

            _cookieContainer.Add(new Cookie("dsid", dsid, "/", "auth.api.swedbank.se"));

            client.BaseAddress = new Uri(_baseUri);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authKey);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("sv-se"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
            client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
            client.DefaultRequestHeaders.Add("Keep-Alive", "true");
            client.DefaultRequestHeaders.TryAddWithoutValidation("User-agent", userAgent);
            
            _client = client;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
        {
            var uri = BuildUrl(url);

            var response = await _client.PostAsync(uri, new ObjectContent<T>(data, new JsonMediaTypeFormatter()
            {
                SerializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.None
                }
            }));
            return response;
        }

        public async Task<HttpResponseMessage> PostAsync(string url)
        {
            var uri = BuildUrl(url);

            uri.SetQueryParam("dsid", _dsid);
            var response = await _client.PostAsync(uri, new StringContent(string.Empty));
            return response;
        }
        public async Task<T> GetAsync<T>(string url)
        {
            var uri = new Flurl.Url(_baseUri + url);
            uri.SetQueryParam("dsid", _dsid);
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            
            return await response.Content.ReadAsAsync<T>();
        }

        public async Task PutAsync(string url)
        {
            var uri = BuildUrl(url);

            var response = await _client.PutAsync(uri, new StringContent(string.Empty));
            response.EnsureSuccessStatusCode();
        }

        private Url BuildUrl(string url)
        {
            var uri = new Flurl.Url(_baseUri + url);
            uri.SetQueryParam("dsid", _dsid);
            return uri;
        }
    }
}
