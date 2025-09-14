using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDAL.Models;

namespace WarehouseDAL.DTOs
{
    public class PickingDTO
    {
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(200,ErrorMessage ="Maxlength is 200")]
        public string Description { get; set; }

        [Required(ErrorMessage = "PickingDate is required")]
        public DateTime PickingDate { get; set; }
    }

    public class ShowPickingDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime PickingDate { get; set; }
        public string Status { get; set; }
        public string SourceWarehouseName { get; set; }
    }
}
