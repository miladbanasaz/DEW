using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace WeatherObservationStation.Tools
{
    public static class Globals
    {
        private static readonly HttpClient _httpClient;

        static Globals()
        {
            _httpClient = new HttpClient();
        }

        public static async Task<string> RequestUrl(string url, HttpMethod method, object postData = null,
                    Dictionary<string, string> headers = null, int timeout = 10_000, ContentType contentType = ContentType.Json)
        {
            var retValue = string.Empty;
            try
            {
                if (!url.StartsWith("http://"))
                    url = $"http://{url}";


                var message = new HttpRequestMessage(method, new Uri(url));

                if (postData != null)
                {
                    if (contentType == ContentType.Json)
                    {
                        message.Content = new StringContent(JsonConvert.SerializeObject(postData), Encoding.UTF8,
                            "application/json");
                    }

                    if (contentType == ContentType.XWwwFormUrlencoded)
                    {
                        //var items = HttpUtility.ParseQueryString(request.Body);
                        message.Content = new FormUrlEncodedContent(postData.ToDictionary<string>());
                    }
                }

                if (headers != null && headers.Count > 0)
                {
                    foreach (var header in headers)
                    {
                        message.Headers.Add(header.Key, header.Value);
                    }
                }

                var cancellationToken = new CancellationTokenSource();
                cancellationToken.CancelAfter(timeout);

                var res = await _httpClient.SendAsync(message, cancellationToken.Token);

                if (res == null)
                    return retValue;

                res.EnsureSuccessStatusCode();

                if (!res.IsSuccessStatusCode)
                {
                    var cnt = string.Empty;
                    if (res.Content != null)
                        cnt = await res.Content?.ReadAsStringAsync();

                    return retValue;
                }

                retValue = await res.Content.ReadAsStringAsync();

            }
            catch (Exception e)
            {
                
            }

            return retValue;
        }

        public static async Task<T> RequestUrl<T>(string url, HttpMethod method, object postData = null,
            Dictionary<string, string> headers = null, int timeout = 10_000, 
            ContentType contentType = ContentType.Json)
        {
            var res = await RequestUrl(url, method, postData, headers, timeout, contentType);//, cancellationToken);
            return string.IsNullOrWhiteSpace(res)
                ? default(T)
                : JsonConvert.DeserializeObject<T>(res);
        }

        public static Dictionary<string, TValue> ToDictionary<TValue>(this object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json);
            return dictionary;
        }
    }

    public enum ContentType
    {
        Json,
        XWwwFormUrlencoded
    }
}