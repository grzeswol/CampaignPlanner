using CampaignPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignPlanner.Services
{
    public class KeywordDataService : IDataService<Keyword>
    {
        public async Task<bool> AddItemAsync(Keyword item)
        {
            using (var context = new CampaignPlannerContext())
            {
                context.Keywords.Add(item);
                await context.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAllItemsAsync()
        {
            using (var context = new CampaignPlannerContext())
            {
                context.RemoveRange(context.Keywords);
                await context.SaveChangesAsync();

            }
            return await Task.FromResult(true);
        }

        public async Task<Keyword> GetItemAsync(int id)
        {
            var result = new Keyword();
            using (var context = new CampaignPlannerContext())
            {
                result = context.Keywords.FirstOrDefault(t => t.Id == id);
            }

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Keyword>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = new List<Keyword>();
            using (var context = new CampaignPlannerContext())
            {
                result = context.Keywords.ToList();
            }

            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateItemAsync(Keyword item)
        {
            throw new NotImplementedException();
        }
    }
}
