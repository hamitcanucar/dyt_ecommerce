using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace dytsenayasar.Util
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> PostAsJsonAsync<T>(this HttpClient client, string url, T data)
        {
            var dataStr = JsonConvert.SerializeObject(data);
            return client.PostAsJsonAsync(url, dataStr);
        }

        public static Task<HttpResponseMessage> PostAsJsonAsync(this HttpClient client, string url, string dataAsJsonStr)
        {
            var content = new StringContent(dataAsJsonStr);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return client.PostAsync(url, content);
        }
    }
}