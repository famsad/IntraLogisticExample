using IntraLogisticCodingExample.Entities;
using IntraLogisticCodingExample.Entities.Logistics;

namespace IntraLogisticCodingExample.Warehouse.Entities
{
    public class StockKeepingUnit : BaseEntity, IEntityInStorage, IUsesTimeStamps
    {
    
        private readonly Location DefaultLocation;
        public StockKeepingUnit() => DefaultLocation = new();

        /// <summary>
        /// Item the SKU contains.
        /// </summary>
        public Item Item { get; set; }
        
        /// <summary>
        /// Quantity of Items in this SKU
        /// </summary>
        public int Quantity { get; set; }

        //these are just here to make use of some Interface stuff.
        public Location currentLocation { get => DefaultLocation; set => nextLocation = DefaultLocation; }
        public Location nextLocation { get => DefaultLocation; set => nextLocation = DefaultLocation; }
        public Location destinationLocation { get => DefaultLocation; set => nextLocation = DefaultLocation; }

        //also mockup as no event triggering is implemented at the moment.
        //But as it is used in the priority determination it is implemented as this mockup.
        public DateTime DateCreated { get; }
        public DateTime DateModified { get; }
    }
}
