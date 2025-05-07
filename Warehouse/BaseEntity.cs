namespace IntraLogisticCodingExample.Entities
{
    public class BaseEntity
    {
      
        /// <summary>
        /// Id of the Entity
        /// </summary>
        public string Id { get; set; } = Guid.NewGuid().ToString();

    }
}
