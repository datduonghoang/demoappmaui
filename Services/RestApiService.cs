using DemoApp.Helpers;
using DemoApp.Models;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DemoApp.Services
{
    internal class RestApiService : IRestApiService
    {
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        private readonly JsonSerializerOptions _deSerializerOptions = new()
        {
            UnmappedMemberHandling = JsonUnmappedMemberHandling.Skip
        };

        private readonly Lazy<HttpClient> _httpClient =
            new Lazy<HttpClient>(
                () =>
                {
                    var httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    return httpClient;
                },
                LazyThreadSafetyMode.ExecutionAndPublication);

        private HttpClient PrepareRestRequest(string token)
        {
            var httpClient = _httpClient.Value;
            if (token != null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = null;
            }
            return httpClient;
        }
        public async Task<ResponseModel<string>> DeleteAsync(string auth, Uri uri)
        {
            var client = PrepareRestRequest(auth);
            var result = new ResponseModel<string>
            {
                Data = default,
                IsSucceed = false,
            };
            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);
                result.IsSucceed = response.IsSuccessStatusCode;
                result.StatusCode = (int)response.StatusCode;
                string message = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(message))
                {
                    result.ErrorMessage = message;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                result.StatusCode = 500;
            }

            return result;
        }

        public async Task<ResponseModel<T>> GetAsync<T>(string auth, Uri uri)
        {
            var client = PrepareRestRequest(auth);
            var result = new ResponseModel<T>
            {
                Data = default,
                IsSucceed = false,
            };
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                Debug.WriteLine(@"\response {0}", response);
                result.IsSucceed = response.IsSuccessStatusCode;
                result.StatusCode = (int)response.StatusCode;
                string content = await response.Content.ReadAsStringAsync();
                if (result.IsSucceed)
                {
                    result.Data = JsonSerializer.Deserialize<T>(content, _deSerializerOptions);
                }
                else
                {
                    result.ErrorMessage = content;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\GetItemAsync Faild {0}", ex.Message);
                result.StatusCode = 500;
            }
            return result;
        }

        public async Task<ResponseModel<List<T>>> GetCollectionAsync<T>(string auth, Uri uri)
        {
            var client = PrepareRestRequest(auth);
            var result = new ResponseModel<List<T>>
            {
                Data = Enumerable.Empty<T>().ToList(),
                IsSucceed = false
            };
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                Debug.WriteLine(@"\GetCollectionAsync response {0}", response);
                result.IsSucceed = response.IsSuccessStatusCode;
                result.StatusCode = (int)response.StatusCode;
                List<T> content = await response.Content.ReadFromJsonAsync<List<T>>();
                if (result.IsSucceed)
                {
                    result.Data = content;
                }
                else
                {
                    result.Data = Enumerable.Empty<T>().ToList();
                    result.ErrorMessage = await response.Content.ReadAsStringAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\GetItemsAsync Faild {0}", ex.Message);
                result.StatusCode = 500;
            }
       
            return result;
        }

        public async Task<ResponseModel<string>> PostAsync<T>(string auth, Uri uri, T item, bool useBodyRequest = true)
        {
            var client = PrepareRestRequest(auth);
            var result = new ResponseModel<string>
            {
                Data = default,
                IsSucceed = false,
            };
            try
            {
                StringContent content = null;
                if (item != null && item.GetType() != typeof(string))
                {
                    if (!useBodyRequest)
                    {
                        uri = new Uri(QueryStringHelper.BuildUrlWithQueryStringUsingStringConcat(uri.AbsoluteUri, QueryStringHelper.ConvertFromObjectToDictionary(item)));
                    }
                    else
                    {
                        string json = JsonSerializer.Serialize<T>(item, _serializerOptions);
                        content = new StringContent(json, Encoding.UTF8, "application/json");
                    }
                }

                HttpResponseMessage response = await client.PostAsync(uri, content);
                result.IsSucceed = response.IsSuccessStatusCode;
                result.StatusCode = (int)response.StatusCode;
                result.ResultMessageString = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(result.ResultMessageString))
                {
                    result.ErrorMessage = result.ResultMessageString;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                result.StatusCode = 500;
            }

            return result;
        }

        public async Task<ResponseModel<string>> PutAsync<T>(string auth, Uri uri, T item)
        {
            var client = PrepareRestRequest(auth);
            var result = new ResponseModel<string>
            {
                Data = default,
                IsSucceed = false,
            };
            try
            {
                StringContent content = null;
                if (item != null)
                {
                    string json = JsonSerializer.Serialize<T>(item, _serializerOptions);
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                HttpResponseMessage response = await client.PutAsync(uri, content);
                result.IsSucceed = response.IsSuccessStatusCode;
                result.StatusCode = (int)response.StatusCode;
                string message = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(message))
                {
                    result.ErrorMessage = message;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                result.StatusCode = 500;
            }

            return result;
        }
    }
}
