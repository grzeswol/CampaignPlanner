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
        public async Task<int> AddItemAsync(Campaign campaign)
        {
            using (var context = new CampaignPlannerContext())
            {
                try
                {
                    var keywords = campaign.Keywords;
                    context.Towns.Attach(campaign.Town);
                    foreach (var keyword in campaign.Keywords)
                    {
                        context.Keywords.Attach(keyword);
                    }
                    context.Campaigns.Add(campaign);
                    return await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<int> DeleteItemAsync(int id)
        {
            using (var context = new CampaignPlannerContext())
            {
                try
                {
                    var campaign = context.Campaigns.FirstOrDefault(t => t.Id == id);
                    context.Campaigns.Remove(campaign);
                    return await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public async Task<int> DeleteAllItemsAsync()
        {
            using (var context = new CampaignPlannerContext())
            {
                try
                {
                    context.RemoveRange(context.Campaigns);
                    return await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {

                    throw ex;
                }


            }
        }

        public async Task<Campaign> GetItemAsync(int id)
        {
            var result = new Campaign();
            using (var context = new CampaignPlannerContext())
            {
                return await context.Campaigns.Include(t => t.Town).Include(t => t.Keywords).FirstOrDefaultAsync(t => t.Id == id);
            }
        }

        public async Task<IEnumerable<Campaign>> GetItemsAsync(bool forceRefresh = false)
        {
            using (var context = new CampaignPlannerContext())
            {
                return await context.Campaigns.ToListAsync();
            }
        }

        public async Task<int> UpdateItemAsync(Campaign item)
        {
            using (var context = new CampaignPlannerContext())
            {
                var dbCampaign = context.Campaigns.FirstOrDefault(t => t.Id == item.Id);
                context.Entry(dbCampaign).CurrentValues.SetValues(item);
                dbCampaign.Keywords.Clear();
                foreach (var keyword in item.Keywords)
                {
                    var dbKeyword = context.Keywords.FirstOrDefault(k => k.Id == keyword.Id);
                    dbCampaign.Keywords.Add(dbKeyword);
                }
                dbCampaign.Town = context.Towns.FirstOrDefault(t => t.Id == item.Town.Id);
                return await context.SaveChangesAsync();
            }
        }

        public IEnumerable<Campaign> GetItems()
        {
            throw new NotImplementedException();
        }

        public Campaign GetItem(int id)
        {
            using (var context = new CampaignPlannerContext())
            {
                return context.Campaigns.Include(t => t.Town).Include(t => t.Keywords).FirstOrDefault(t => t.Id == id);
            }
        }
    }
}
