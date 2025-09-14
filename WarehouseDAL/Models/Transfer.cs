using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.Models
{
    public enum TransferStatus
    {
        OnHold,
        Processed,
        Completed
    }

    public class Transfer : BaseClass
    {
        public TransferStatus Status { get; set; }

        public int PickingId { get; set; }
        public Picking Picking { get; set; }

        public int SourceWarehouseId { get; set; }
        public Warehouse SourceWarehouse { get; set; }

        public int DestinationWarehouseId { get; set; }
        public Warehouse DestinationWarehouse { get; set; }

        public ICollection<TransferItem> TransferItems { get; set; }
    }
}
