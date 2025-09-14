using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.Models
{
    public class BoxItem : BaseClass
    {
        public int Quantity { get; set; }

        public int BoxId { get; set; }
        public Box Box { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }
    }
}
