using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntraLogisticCodingExample.Warehouse.Entities
{
    public interface IUsesTimeStamps
    {
        /// <summary>
        /// DateTime of the creation of the Instance
        /// </summary>
        public DateTime DateCreated { get; }

        /// <summary>
        /// DateTime of the modification of the Instance
        /// </summary>
        public DateTime DateModified { get; }

    }
}
