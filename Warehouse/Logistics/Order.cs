

using IntraLogisticCodingExample.Entities;
using IntraLogisticCodingExample.Warehouse.Constants;
using IntraLogisticCodingExample.Warehouse.Entities;

namespace IntraLogisticCodingExample.Warehouse.Logistics;

public class Order : BaseEntity, IUsesTimeStamps
{
    /// <summary>
    /// Flag if the delivery is required to be fullfilled in one go.
    /// </summary>
    public bool IsCompleteDeliveryRequired { get; set; } = false;

    /// <summary>
    /// Priority of the <see cref="Order"/>
    /// </summary>
    public Priority Priority { get; set; } = Priority.Nomal;

    /// <summary>
    /// List of different Items that the order contains
    /// </summary>
    public List<Item> Items { get; set; } = new List<Item>();

    /// <summary>
    /// Current State of the Order
    /// </summary>
    public OrderState State { get; set; } = OrderState.None;

    //also mockup as no event triggering is implemented at the moment.
    //But as it is used in the priority determination it is implemented as this mockup.
    public DateTime DateCreated { get; } = DateTime.UtcNow();
    public DateTime DateModified { get; set; } = DateTime.UtcNow();
}

