using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDAL.DTOs;

namespace WarehouseDAL.Interfaces
{
    public interface ILotService
    {
        public Task<int> AssignBoxesToLot(AssignBoxToLotDTO dto);
        public Task<List<LotDTO>> GetAllLots();
        public Task AssignBoxesToLotAutomatic();
        public Task ReadyToShip(int transferId);
        public Task InTransit(int transferId, string destinationWarehouseName);
        public Task ApproveShipped(int transferId);
    }
}
