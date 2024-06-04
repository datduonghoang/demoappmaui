using DemoApp.Models;

namespace DemoApp.Services
{
    public interface IRestApiService
    {
        Task<ResponseModel<List<T>>> GetCollectionAsync<T>(string auth, Uri uri);
        Task<ResponseModel<T>> GetAsync<T>(string auth, Uri uri);
        Task<ResponseModel<string>> PostAsync<T>(string auth, Uri uri, T item, bool useBodyRequest = true);
        Task<ResponseModel<string>> PutAsync<T>(string auth, Uri uri, T item);
        Task<ResponseModel<string>> DeleteAsync(string auth, Uri uri);
    }
}
