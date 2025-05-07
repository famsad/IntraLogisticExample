using IntraLogisticCodingExample.Warehouse.Constants;
using IntraLogisticCodingExample.Warehouse.Entities;
using IntraLogisticCodingExample.Warehouse.Logistics;
using Microsoft.EntityFrameworkCore;

namespace IntraLogisticCodingExample;

public class Program
{
    public DbContextOptions<AppDbContext> DbContextOptions { get; set; }

    public Program()
    {
        DbContextOptions = provideDbOptions();
    }
    public void addOptionsAndData()
    {

        // Add example data
        using (var context = new AppDbContext(DbContextOptions))
        {
            context.Items.Add(new Item { ProductNumber = "Juice01", Quantity = 5 });
            context.Items.Add(new Item { ProductNumber = "Juice02", Quantity = 5 });
            context.Items.Add(new Item { ProductNumber = "Juice03", Quantity = 10 });
            context.StockKeepingUnits.Add(new StockKeepingUnit { Item = context.Items.First(), Quantity = 10 });
            context.StockKeepingUnits.Add(new StockKeepingUnit { Item = context.Items.First(), Quantity = 10 });
            context.StockKeepingUnits.Add(new StockKeepingUnit { Item = context.Items.First(), Quantity = 5 });
            context.StockKeepingUnits.Add(new StockKeepingUnit { Item = context.Items.Last(), Quantity = 10 });
            context.Orders.Add(new Order { State = OrderState.ReadyToDeliver, IsCompleteDeliveryRequired = false, Priority = Priority.Nomal, Items = new List<Item> { context.Items.First(), context.Items.Last() } });
            context.Orders.Add(new Order { State = OrderState.Released, IsCompleteDeliveryRequired = true, Priority = Priority.Low, Items = new List<Item> { context.Items.Last(), context.Items.Last() } });
            context.Orders.Add(new Order { State = OrderState.Finished, IsCompleteDeliveryRequired = true, Priority = Priority.Low, Items = new List<Item> { context.Items.Last(), context.Items.Last() } });
            context.Orders.Add(new Order { State = OrderState.Released, IsCompleteDeliveryRequired = false, Priority = Priority.High, Items = new List<Item> { context.Items.First(), context.Items.First() } });
            context.SaveChanges();
        }
    }

    public DbContextOptions<AppDbContext> createDbOptions()
    {
        return new DbContextOptionsBuilder<AppDbContext>().Options;
    }
    public DbContextOptions<AppDbContext> provideDbOptions()
    {
        return DbContextOptions;
    }


}