using CampaignPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignPlanner.Services
{
    public class CampaignDataService : IDataService<Campaign>
    {
        public async Task<bool> AddItemAsync(Campaign campaign)
        {
            using (var context = new CampaignPlannerContext())
            {
                try
                {
                    var town = context.Towns.FirstOrDefault(t => t.Id == campaign.Town.Id);
                    context.Towns.Attach(town);
                    campaign.Town = town;
                    context.Campaigns.Add(campaign);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            using (var context = new CampaignPlannerContext())
            {
                try
                {
                    var campaign = context.Campaigns.FirstOrDefault(t => t.Id == id);
                    context.Campaigns.Remove(campaign);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteAllItemsAsync()
        {
            using (var context = new CampaignPlannerContext())
            {
                try
                {
                    context.RemoveRange(context.Campaigns);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw;
                }


            }
            return await Task.FromResult(true);
        }

        public async Task<Campaign> GetItemAsync(int id)
        {
            var result = new Campaign();
            using (var context = new CampaignPlannerContext())
            {
                result = context.Campaigns.Include(t => t.Town).FirstOrDefault(t => t.Id == id);
            }

            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Campaign>> GetItemsAsync(bool forceRefresh = false)
        {
            var result = new List<Campaign>();
            using (var context = new CampaignPlannerContext())
            {
                result = context.Campaigns.ToList();
            }

            return await Task.FromResult(result);
        }

        public async Task<bool> UpdateItemAsync(Campaign item)
        {
            using (var context = new CampaignPlannerContext())
            {
                var dbCampaign = context.Campaigns.Include(t => t.Town).FirstOrDefault(t => t.Id == item.Id);
                var IsNewTown = dbCampaign.Town.Id != item.Town.Id;

                context.Entry(dbCampaign).CurrentValues.SetValues(item);
                if (IsNewTown)
                {
                    var town = context.Towns.FirstOrDefault(t => t.Id == item.Town.Id);
                    dbCampaign.Town = town;
                }
                await context.SaveChangesAsync();
            }
            return await Task.FromResult(true);
        }
    }
}
