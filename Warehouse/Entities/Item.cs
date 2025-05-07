

using IntraLogisticCodingExample.Entities;

namespace IntraLogisticCodingExample.Warehouse.Entities
{
    public class Item : BaseEntity
    {
        /// <summary>
        /// ProductNumber of the Item. May be from external source.
        /// </summary>
        public string ProductNumber { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// Quantity of products in one Item.
        /// </summary>
        public int Quantity { get; set; } = 0;

    }
}
