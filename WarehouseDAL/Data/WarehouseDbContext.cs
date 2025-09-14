using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WarehouseDAL.Models;

namespace WarehouseDAL.Data
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Box> Boxes { get; set; }
        public DbSet<BoxItem> BoxItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Lot> Lots { get; set; }
        public DbSet<Picking> Pickings { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TransferItem> TransferItems {get;set;}
        public DbSet<Warehouse> Warehouses { get; set; }
    }
}
