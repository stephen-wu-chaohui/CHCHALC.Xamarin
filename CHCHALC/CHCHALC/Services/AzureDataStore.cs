using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using CHCHALC.Models;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;

namespace CHCHALC.Services
{
    public class AzureDataStore<T> : IDataStore<T> where T : Entity
    {
        public HttpClient client;
        public IEnumerable<T> items = new List<T>();
        public readonly string controller;

        public void SetToken(string token)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public AzureDataStore(string controller)
        {
            client = new HttpClient {
                BaseAddress = new Uri($"{App.AzureBackendUrl}/")
            };
            this.controller = controller;
        }

        protected bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public async Task<IEnumerable<T>> GetItemsAsync(string component, IDictionary<string, string> condistions)
        {
            if (IsConnected) {
                try {
                    var resource = $"api/{controller}";
                    if (!string.IsNullOrEmpty(component)) {
                        resource += $"/{component}";
                    }
                    if (condistions != null && condistions.Count > 0) {
                        resource += '?' + string.Join("&", condistions.Select(kv => $"{kv.Key}={kv.Value}"));
                    }
                    var json = await client.GetStringAsync(resource);
                    items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(json));
                } catch (Exception ex) {
                    Debug.WriteLine(ex.Message);
                }
            }

            return items;
        }

        public async Task<T> GetItemAsync(int id)
        {
            if (id != 0 && IsConnected) {
                var json = await client.GetStringAsync($"api/{controller}/{id}");
                return await Task.Run(() => JsonConvert.DeserializeObject<T>(json));
            }

            return null;
        }

        virtual public async Task<T> AddItemAsync(T item, string component)
        {
            if (item == null || !IsConnected)
                return null;

            var endPoint = string.IsNullOrEmpty(component) ? $"api/{controller}" : $"api/{controller}/{component}";
            var serializedItem = JsonConvert.SerializeObject(item);

            using (HttpResponseMessage response = await client.PostAsync(endPoint, new StringContent(serializedItem, Encoding.UTF8, "application/json"))) {
                return response.IsSuccessStatusCode ? JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) : null;
            }
        }

        virtual public async Task<bool> UpdateItemAsync(T item, string component)
        {
            if (item == null || item.Id == 0)
                return false;

            try {
                var stringContent = new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");
                var endPoint = string.IsNullOrEmpty(component) ? $"api/{controller}/{item.Id}" : $"api/{controller}/{component}/{item.Id}";
                var response = await client.PutAsync(new Uri(endPoint, UriKind.Relative), stringContent);
                return response.IsSuccessStatusCode;
            } catch (Exception ex) {
                Debug.WriteLine(ex.Message);
            }
            return false;
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            if (id == 0 && !IsConnected)
                return false;

            var response = await client.DeleteAsync($"api/{controller}/{id}");

            return response.IsSuccessStatusCode;
        }

        virtual public async Task<T> Upsert(T item, string component)
        {
            if (item.Id == 0) {
                return await AddItemAsync(item, component);
            } else {
                await UpdateItemAsync(item, component);
                return item;
            }
        }

    }

}
