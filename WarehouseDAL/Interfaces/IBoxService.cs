using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDAL.DTOs;

namespace WarehouseDAL.Interfaces
{
    public interface IBoxService
    {
        public Task AssignItemsToBox(List<AssignItemsToBoxDTO> dtos);
        public Task<List<ShowBoxItemsDTO>> ShowBoxItems();
        public Task ConfirmBoxes(int transferId);
    }
}
