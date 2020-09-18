using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace dyt_ecommerce.Util
{
    public static class HttpResponseExtensions
    {
        public static void AddCount(this HttpResponse response, long count)
        {
            response.Headers.Add("Count", count.ToString());
        }
    }
}