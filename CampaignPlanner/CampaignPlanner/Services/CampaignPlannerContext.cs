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
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>()
              .HasOne(t => t.Town)
              .WithMany()
              .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Campaign>()
             .HasMany(t => t.Keywords)
             .WithOne()
             .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
