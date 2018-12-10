using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Carton.Client.Utilities;

namespace Carton.Client.Utilities
{
    internal class Http : IHttp
    {
        private readonly HttpClient client;

        public Http()
        {
            client = new HttpClient(new HttpClientHandler { UseDefaultCredentials = false });
        }

        public async Task<Response<TOut>> Post<TIn, TOut>(string address, TIn content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(address),
                Headers = {{ "Accept", "application/json" }},
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8)
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await Send<TOut>(request);
        }

        public async Task<Response<TOut>> Put<TIn, TOut>(string address, TIn content)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri(address),
                Headers = {{ "Accept", "application/json" }},
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8)
            };

            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await Send<TOut>(request);
        }

        public async Task<Response<TOut>> Get<TOut>(string address)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(address),
                Headers = {{ "Accept", "application/json" }},
            };

            return await Send<TOut>(request);
        }

        public async Task<Response<T>> Delete<T>(string address)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(address),
                Headers = {{ "Accept", "application/json" }},
            };

            return await Send<T>(request);
        }

        private async Task<Response<T>> Send<T>(HttpRequestMessage request)
        {
            try
            {
                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                return new Response<T>
                {
                    StatusCode = (int)response.StatusCode,
                    Error = response.ReasonPhrase,
                    Data = JsonConvert.DeserializeObject<T>(content)
                };
            }
            catch(Exception exception)
            {
                return new Response<T>
                {
                    StatusCode = 0,
                    Error = exception.Message,
                    Data = default(T)
                };
            }
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}
