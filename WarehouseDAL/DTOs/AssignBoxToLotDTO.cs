using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.DTOs
{
    public class AssignBoxToLotDTO
    {
        public List<int> BoxeIds { get; set; }
    }

    public class LotDTO
    {
        public int LotId { get; set; }
        public List<int> BoxIds { get; set; }
        public string Label { get; set; }
        public string LotStatus { get; set; }
        public string ShipmentStatus { get; set; }
    }

    public class ShipmentDTO
    {
        public List<int> LotIds { get; set; }
    }
}
