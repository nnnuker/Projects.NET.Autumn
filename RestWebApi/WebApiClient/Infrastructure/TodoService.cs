using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApiClient.Models;

namespace WebApiClient.Infrastructure
{
    public class TodoService
    {
        private readonly HttpClient httpClient;
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["TodoServiceUrl"];

        public TodoService()
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<IEnumerable<TodoItemModel>> GetAll()
        {
            var dataAsString = await httpClient.GetStringAsync(serviceApiUrl);
            return JsonConvert.DeserializeObject<IEnumerable<TodoItemModel>>(dataAsString);
        }

        public async Task<TodoItemModel> Get(int id)
        {
            var dataAsString = await httpClient.GetStringAsync($"{serviceApiUrl}/{id}");
            return JsonConvert.DeserializeObject<TodoItemModel>(dataAsString);
        }

        public async Task UpdateItem(int id, TodoItemModel item)
        {
            var result = await httpClient.PutAsJsonAsync($"{serviceApiUrl}/{id}", item);
            result.EnsureSuccessStatusCode();
        }

        public async Task<TodoItemModel> AddItem(TodoItemModel item)
        {
            var result = await httpClient.PostAsJsonAsync(serviceApiUrl, item);
            result.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<TodoItemModel>(await result.Content.ReadAsStringAsync());
        }

        public async Task RemoveItem(int id)
        {
            var result = await httpClient.DeleteAsync($"{serviceApiUrl}/{id}");
            result.EnsureSuccessStatusCode();
        }
    }
}