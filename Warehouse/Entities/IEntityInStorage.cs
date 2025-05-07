
using IntraLogisticCodingExample.Entities.Logistics;

namespace IntraLogisticCodingExample.Warehouse.Entities
{
    /// <summary>
    /// This Interface includes all Information about the movement in the Storage/Warehouse.
    /// </summary>
    internal interface IEntityInStorage
    {
        /// <summary>
        /// current location in the Warehouse
        /// </summary>
        public Location currentLocation { get; set; }

        /// <summary>
        /// planned next location for the movement in the warehouse
        /// </summary>
        public Location nextLocation { get; set; }

        /// <summary>
        /// destination for the movement in the warehouse. 
        /// </summary>
        public Location destinationLocation { get; set; }
    }
}
