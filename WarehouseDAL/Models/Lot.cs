using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.Models
{
    public enum ShipmentStatus
    {
        Ready,
        InTransit,
        Completed
    }

    public enum LotStatus
    {
        Open,
        Close
    }

    public class Lot : BaseClass
    {
        public string LotCode { get; set; }
        public ShipmentStatus ShipmentStatus { get; set; }
        public LotStatus LotStatus { get; set; }

        public ICollection<Box> Boxes { get; set; }
    }
}
