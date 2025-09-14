using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseDAL.DTOs
{
    public class TransferDTO
    {
        [Required(ErrorMessage = "Picking Id is required")]
        public int PickingId { get; set; }

        [Required(ErrorMessage = "Warehouse is required")]
        public string DestinationWarehouseName { get; set; }
    }

    public class ShowTransferDTO
    {
        public int Id { get; set; }
        public int PickingId { get; set; }
        public string SourceWarehouseName { get; set; }
        public string DestinationWarehouseName { get; set; }
        public string Status { get; set; }
    }

    public class AssignItemsToTransfer
    {
        public int TransferId { get; set; }
        public int ItemId { get; set; }
        public int QuantityRequired { get; set; }
    }

    public class TransferItemDTO
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Barcode { get; set; }
        public int QuantityRequired { get; set; }
        public int QuantityScanned { get; set; }
    }

    public class ScanItemsDTO
    {
        public int TransferId { get; set; }
        public string BarCode { get; set; }
    }
}
