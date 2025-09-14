using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.DTOs
{
    public class AssignItemsToBoxDTO
    {
        public int TransferId { get; set; }
        public List<BoxItemsDTO> Items { get; set; }
    }

    public class BoxItemsDTO
    {
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }

    public class ShowBoxItemsDTO
    {
        public int Id { get; set; }
        public int BoxId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
    }
}
