using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignPlanner.Services
{
    public interface IDataService<T>
    {
        Task<int> AddItemAsync(T item);
        Task<int> UpdateItemAsync(T item);
        Task<int> DeleteItemAsync(int id);
        Task<int> DeleteAllItemsAsync();
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
        IEnumerable<T> GetItems();
        T GetItem(int id);
    }
}
