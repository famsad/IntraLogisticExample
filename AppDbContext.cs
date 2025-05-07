using IntraLogisticCodingExample.Entities.Logistics;
using IntraLogisticCodingExample.Warehouse.Entities;
using IntraLogisticCodingExample.Warehouse.Logistics;
using Microsoft.EntityFrameworkCore;

namespace IntraLogisticCodingExample;

public class AppDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<StockKeepingUnit> StockKeepingUnits { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Order> Orders { get; set; }

     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
          
}
