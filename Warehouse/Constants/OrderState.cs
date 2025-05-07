

namespace IntraLogisticCodingExample.Warehouse.Constants;

public enum OrderState
{
    None,
    Released,
    ReadyToDeliver,
    WaitForRestock,
    Finished
}
