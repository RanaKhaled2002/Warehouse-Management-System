using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.Models
{
    public class Box : BaseClass
    {
        public string Label { get; set; }

        public int TransferId { get; set; }
        public Transfer Transfer { get; set; }

        public int? LotId { get; set; }
        public Lot Lot { get; set; }

        public ICollection<BoxItem> BoxItems { get; set; }
    }
}
