using IntraLogisticCodingExample.Warehouse.Constants;
using IntraLogisticCodingExample.Warehouse.Entities;
using IntraLogisticCodingExample.Warehouse.Logistics;

namespace IntraLogisticCodingExample.Workflow;

public class DoFeedOutBasedOnOrder
{
    public AppDbContext Context { get; set; }
    public List<Item> Items { get; set; }

    public DoFeedOutBasedOnOrder(AppDbContext context)
    {
        Context = context;
    }

    public void main()
    {
        var orders = getValidOrdersInCorrectOrder();
        foreach (var order in orders)
        {
            var items = getItemsForOrder(order);
            if (CheckSufficientQuantityIfCompleteFullfillmentRequired(items, order))
            {
                DeleteItemsFromStorageAndUpdateOrder(items, order);
            }
            else
            {
                CompleteOrderAsAvailable(items, order);
                MarkOrderAsWaitForRestock(order);
            }
        }
    }

    /// <summary>
    /// get all open orders in correct order to continue fullfillment.
    /// </summary>
    /// <returns></returns>
    public List<Order> getValidOrdersInCorrectOrder()
    {
        var orders = Context.Orders.ToList();
        return orders.Where(o => o.State == OrderState.Released || o.State == OrderState.WaitForRestock).OrderBy(o => o.Priority).ThenBy(o => o.DateCreated).ToList();
    }

    /// <summary>
    /// Get all items for a given order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public List<Item> getItemsForOrder(Order order)
    {
        var items = new List<Item>();
        var Skus = Context.StockKeepingUnits.ToList();
        //get all Items of the Order and Check if they are present in any sku. No QuantityCheck yet
        foreach (var item in order.Items)
        {
            items.AddRange(Context.Items.Where(i => i.ProductNumber == item.ProductNumber && Skus.Any(s => s.Id == i.Id)).ToList());
        }
        return items;
    }

    /// <summary>
    /// Check if sufficient stuff is in storage if complete fullfillment is required.
    /// </summary>
    /// <param name="items"> All items of a given Order</param>
    /// <param name="order">given order</param>
    /// <returns></returns>
    public bool CheckSufficientQuantityIfCompleteFullfillmentRequired(List<Item> items, Order order)
    {
        var sku = Context.StockKeepingUnits.ToList();
        if (order.IsCompleteDeliveryRequired)
        {
            foreach (var item in items)
            {
                var availableSkus = sku.Where(s => s.Item.ProductNumber == item.ProductNumber && !s.currentLocation.IsLocked).ToList();

                int availableItemAmount = 0;
                foreach (var availableSku in availableSkus)
                {
                    availableItemAmount = availableItemAmount + availableSku.Item.Quantity;
                }
                if (item.Quantity > availableItemAmount)
                {
                    return false;
                }
            }
        }
        return true;
    }

    /// <summary>
    /// Removes all used Items out of the storage and updates the Order afterwards as ReadyToDeliver
    /// </summary>
    /// <param name="items"> All items of a given Order</param>
    /// <param name="order">given order</param>
    public void DeleteItemsFromStorageAndUpdateOrder(List<Item> items, Order order)
    {
        var sku = Context.StockKeepingUnits.ToList();
        foreach (var item in items)
        {
            var availableSkus = sku.Where(s => s.Item.ProductNumber == item.ProductNumber && !s.currentLocation.IsLocked).ToList();
            var amountToBookAway = item.Quantity;

            while (amountToBookAway != 0)
            {
                if (amountToBookAway > availableSkus.First().Item.Quantity)
                {
                    amountToBookAway = amountToBookAway - availableSkus.First().Item.Quantity;
                    availableSkus.Remove((StockKeepingUnit?)availableSkus.First());
                }
                else
                {
                    availableSkus.First().Item.Quantity = availableSkus.First().Item.Quantity - amountToBookAway;
                    amountToBookAway = 0;
                }
            }
        }
        order.State = OrderState.ReadyToDeliver;
    }

    /// <summary>
    /// Complete the Order as much as possible based on Items that are available in the warehouse. 
    /// </summary>
    /// <param name="items"> All items of a given Order</param>
    /// <param name="order">given order</param>
    public void CompleteOrderAsAvailable(List<Item> items, Order order)
    {
        var sku = Context.StockKeepingUnits.ToList();
        foreach (var item in items)
        {
            var availableSkus = sku.Where(s => s.Item.ProductNumber == item.ProductNumber && !s.currentLocation.IsLocked).ToList();
            var amountToBookAway = item.Quantity;

            while (amountToBookAway != 0 || availableSkus.Count!=0)
            {
                if (amountToBookAway > availableSkus.First().Item.Quantity)
                {
                    amountToBookAway = amountToBookAway - availableSkus.First().Item.Quantity;
                    availableSkus.Remove((StockKeepingUnit?)availableSkus.First());
                }
                else
                {
                    availableSkus.First().Item.Quantity = availableSkus.First().Item.Quantity - amountToBookAway;
                    amountToBookAway = 0;
                }
            }
            if (amountToBookAway == 0)
            {
                order.Items.Remove(item);
            }
            else
            {
                order.Items.Remove(item);
                item.Quantity = amountToBookAway;
                order.Items.Add(item);
            }
        }
        order.Items = items;
    }

    /// <summary>
    /// Wait for restock if insufficient Items are in the warehouse.
    /// </summary>
    /// <param name="order">given order</param>
    public void MarkOrderAsWaitForRestock(Order order)
    {
        order.State = OrderState.WaitForRestock;
    }
}
