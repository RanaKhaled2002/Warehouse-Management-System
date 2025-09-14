using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.Models
{
    public enum PickingStatus
    {
        Created,
        Sorted,
        Boxed,
        Completed
    }

    public class Picking : BaseClass
    {
        public string Description { get; set; }
        public DateTime PickingDate { get; set; }
        public PickingStatus Status { get; set; }

        public int WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; }    

        public ICollection<Transfer> Transfers { get; set; }
    }
}
