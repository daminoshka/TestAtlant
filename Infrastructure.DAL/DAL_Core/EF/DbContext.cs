using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Configuration;
using Infrastructure.DAL.DAL_Core.Model;

namespace Infrastructure.DAL.DAL_Core.EF
{
    public partial class EntitiesContext : DbContext
    {
        public virtual DbSet<Detail> Detail { get; set; }
        public virtual DbSet<Storekeeper> Storekeeper { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
             .UseSqlServer("Server=DESKTOP-86AVPAP;Database=TestDb;User Id=testAtlant;Password=ATLANT;Trusted_Connection=True;");
        }
    }
}
