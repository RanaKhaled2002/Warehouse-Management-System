using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDAL.DTOs;

namespace WarehouseDAL.Interfaces
{
    public interface ITransferService
    {
        public Task<int> CreateTransferAsync(TransferDTO dto);
        public Task<List<ShowTransferDTO>> GetAllTransfer();
        public Task<List<ShowTransferDTO>> GetTransferWithOnholdStatusAsync(string status,string warehouseName);
        public Task<List<ShowTransferDTO>> GetTransferWithSpecificPickingStatusAsync(string status);
        public Task AssignItemsToTransfer(List<AssignItemsToTransfer> dtos);
        public Task<List<TransferItemDTO>> GetTransferItemsAsync(int transferId);
        public Task ScanItemAsync(ScanItemsDTO dto);
        public Task ConfirmTransferAsync(int transferId);
        public Task ConfirmSorted(int transferId);
    }
}
