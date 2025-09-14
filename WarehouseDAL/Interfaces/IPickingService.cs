using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseDAL.DTOs;
using WarehouseDAL.Models;

namespace WarehouseBLL.Interfaces
{
    public interface IPickingService
    {
        public Task<int> CreatePickingAsync(string warehouseName,PickingDTO dto);
        public Task<List<ShowPickingDTO>> GetAllPickingAsync();
    }
}
