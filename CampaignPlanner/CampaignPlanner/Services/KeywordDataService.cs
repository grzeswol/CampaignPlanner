using CampaignPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignPlanner.Services
{
    public class KeywordDataService : IDataService<Keyword>
    {
        public async Task<int> AddItemAsync(Keyword item)
        {
            using (var context = new CampaignPlannerContext())
            {
                context.Keywords.Add(item);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAllItemsAsync()
        {
            using (var context = new CampaignPlannerContext())
            {
                context.RemoveRange(context.Keywords);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<Keyword> GetItemAsync(int id)
        {
            var result = new Keyword();
            using (var context = new CampaignPlannerContext())
            {
                return await context.Keywords.FirstOrDefaultAsync(t => t.Id == id);
            }
        }


        public async Task<IEnumerable<Keyword>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = new List<Keyword>();
            using (var context = new CampaignPlannerContext())
            {
                return await context.Keywords.ToListAsync();
            }
        }

        public IEnumerable<Keyword> GetItems()
        {
            using (var context = new CampaignPlannerContext())
            {
                return context.Keywords.ToList();
            }
        }

        public async Task<int> UpdateItemAsync(Keyword item)
        {
            throw new NotImplementedException();
        }

        public Keyword GetItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
