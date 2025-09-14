using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.DTOs
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string BarCode { get; set; }
        public string Name { get; set; }
    }
}
