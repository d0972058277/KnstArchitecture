using System.Text.Json;
using System.Threading.Tasks;

namespace System.Net.Http
{
    public static class HttpContentExtension
    {
        public static async Task<T> ReasAsAsync<T>(this HttpContent httpContent)
        {
            var data = await httpContent.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<T>(data);
            return result;
        }
    }
}
