using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CHCHALC.Services
{
    public interface IDataStore<T>
    {
        void SetToken(string token);
        Task<T> AddItemAsync(T item, string component = null);
        Task<bool> UpdateItemAsync(T item, string component = null);
        Task<T> Upsert(T item, string component = null);
        Task<bool> DeleteItemAsync(int id);
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(string component = null, IDictionary<string, string> condistions = null);
    }
}
