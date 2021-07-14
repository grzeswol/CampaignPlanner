using CampaignPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignPlanner.Services
{
    public class TownDataService : IDataService<Town>
    {
        public async Task<bool> AddItemAsync(Town item)
        {
            using (var context = new CampaignPlannerContext())
            {
                context.Towns.Add(item);
                await context.SaveChangesAsync();
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            using (var context = new CampaignPlannerContext())
            {
                var town = context.Towns.FirstOrDefault(t => t.Id == id);
                context.Towns.Remove(town);
                await context.SaveChangesAsync();

            }
            return await Task.FromResult(true);
        }


        public async Task<bool> DeleteAllItemsAsync()
        {
            using (var context = new CampaignPlannerContext())
            {
                context.RemoveRange(context.Towns);
                await context.SaveChangesAsync();

            }
            return await Task.FromResult(true);
        }

        public async Task<Town> GetItemAsync(int id)
        {
            var result = new Town();
            using (var context = new CampaignPlannerContext())
            {
                result = context.Towns.FirstOrDefault(t => t.Id == id);
            }

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Town>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = new List<Town>();
            using (var context = new CampaignPlannerContext())
            {
                result = context.Towns.ToList();
            }

            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateItemAsync(Town item)
        {
            using (var context = new CampaignPlannerContext())
            {
                var town = context.Towns.FirstOrDefault(t => t.Id == item.Id);
                town.Name = item.Name;
                await context.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }
    }
}
