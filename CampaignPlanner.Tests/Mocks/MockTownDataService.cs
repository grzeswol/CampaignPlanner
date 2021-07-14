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

        public async Task<bool> AddItemAsync(Town item)
        {
            mockTowns.Add(item);
            return await Task.FromResult(true);
        }

        public Task<bool> DeleteAllItemsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Town> GetItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Town>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(mockTowns);
        }

        public async Task<bool> UpdateItemAsync(Town item)
        {
            throw new NotImplementedException();
        }
    }
}
