using CampaignPlanner.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace CampaignPlanner.Services
{
    public class CampaignPlannerContext : DbContext
    {
        public DbSet<Town> Towns { get; set; }

        public CampaignPlannerContext()
        {
            SQLitePCL.Batteries_V2.Init();

            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "campaignplanner.db3");

            optionsBuilder
                .UseSqlite($"Filename={dbPath}");
        }
    }
}
