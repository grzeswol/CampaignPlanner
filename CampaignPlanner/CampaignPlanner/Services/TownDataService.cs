using CampaignPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignPlanner.Services
{
    public class TownDataService : IDataService<Town>
    {
        public async Task<int> AddItemAsync(Town item)
        {
            using (var context = new CampaignPlannerContext())
            {
                context.Towns.Add(item);
                return await context.SaveChangesAsync();
            }
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            using (var context = new CampaignPlannerContext())
            {
                var town = context.Towns.FirstOrDefault(t => t.Id == id);
                context.Towns.Remove(town);
                return await context.SaveChangesAsync();

            }
        }


        public async Task<int> DeleteAllItemsAsync()
        {
            using (var context = new CampaignPlannerContext())
            {
                context.RemoveRange(context.Towns);
                return await context.SaveChangesAsync();

            }
        }

        public async Task<Town> GetItemAsync(int id)
        {
            var result = new Town();
            using (var context = new CampaignPlannerContext())
            {
                return await context.Towns.FirstOrDefaultAsync(t => t.Id == id);
            }
        }

        public async Task<IEnumerable<Town>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = new List<Town>();
            using (var context = new CampaignPlannerContext())
            {
                return await context.Towns.ToListAsync();
            }
        }

        public IEnumerable<Town> GetItems()
        {
            using (var context = new CampaignPlannerContext())
            {
                return context.Towns.ToList();
            }
        }

        public async Task<int> UpdateItemAsync(Town item)
        {
            using (var context = new CampaignPlannerContext())
            {
                var town = context.Towns.FirstOrDefault(t => t.Id == item.Id);
                town.Name = item.Name;
                return await context.SaveChangesAsync();
            }
        }

        public Town GetItem(int id)
        {
            throw new NotImplementedException();
        }
    }
}
