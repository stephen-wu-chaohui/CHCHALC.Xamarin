using System.Collections.Generic;
using System.Threading.Tasks;
using CHCHALC.Models;

namespace CHCHALC.Services
{
    public interface IMockedDataStore<T>
    {
        Task<bool> AddItemAsync(Item item);
        Task<bool> DeleteItemAsync(string id);
        Task<Item> GetItemAsync(string id);
        Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false);
        Task<bool> UpdateItemAsync(Models.Item item);
    }
}