namespace IntraLogisticCodingExample.Entities.Logistics
{
    public class Location : BaseEntity
    {
        /// <summary>
        /// Flag if the SKU on Location is useable to fullfill a order.
        /// </summary>
        public bool IsLocked { get; set; } = false;

    }
}
