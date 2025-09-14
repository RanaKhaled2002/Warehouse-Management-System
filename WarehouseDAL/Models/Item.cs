using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.Models
{
    public class Item : BaseClass
    {
        public string Name { get; set; }
        public string BarCode { get; set; }

        public ICollection<TransferItem> TransferItems { get; set; }
        public ICollection<BoxItem> BoxItems { get; set; }
    }
}
