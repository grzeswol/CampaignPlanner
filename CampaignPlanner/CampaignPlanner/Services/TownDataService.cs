﻿using CampaignPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignPlanner.Services
{
    public class TownDataService : ITownDataService<Town>
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
            throw new NotImplementedException();
        }

        public async Task<Town> GetItemAsync(int id)
        {
            throw new NotImplementedException();

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
            throw new NotImplementedException();
        }
    }
}
