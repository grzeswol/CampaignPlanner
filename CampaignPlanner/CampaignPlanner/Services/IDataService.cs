using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CampaignPlanner.Services
{
    public interface IDataService<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(int id);
        Task<bool> DeleteAllItemsAsync();
        Task<T> GetItemAsync(int id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
