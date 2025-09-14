using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.Models
{
    public class TransferItem : BaseClass
    {
        public int RequestedQty { get; set; }
        public int ScannedQty { get; set; }

        public int TransferId { get; set; }
        public Transfer Transfer { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
