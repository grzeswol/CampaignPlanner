using CampaignPlanner.Models;
using CampaignPlanner.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CampaignPlanner.Tests.Mocks
{
    class MockTownDataService : IDataService<Town>
    {
        private List<Town> mockTowns = new List<Town>()
        {
            new Town { Name = "New York"},
            new Town { Name = "Warsaw"},
            new Town { Name = "London"},
            new Town { Name = "Moscow"},
            new Town { Name = "Los Angeles"}
        };

        public async Task<int> AddItemAsync(Town item)
        {
            mockTowns.Add(item);
            return await Task.FromResult(0);
        }

        public Town GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Town> GetItemAsync(int id)
        {
            var result = mockTowns.Find(t => t.Id == id);
            return Task.FromResult(result);
        }

        public IEnumerable<Town> GetItems()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Town>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(mockTowns);
        }

        Task<int> IDataService<Town>.DeleteAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        Task<int> IDataService<Town>.DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<int> IDataService<Town>.UpdateItemAsync(Town item)
        {
            throw new NotImplementedException();
        }
    }
}
