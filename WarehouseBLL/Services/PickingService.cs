using Microsoft.EntityFrameworkCore;
using System.Net;
using WarehouseBLL.Exceptions;
using WarehouseBLL.Interfaces;
using WarehouseDAL.Data;
using WarehouseDAL.DTOs;
using WarehouseDAL.Models;

namespace WarehouseBLL.Services
{
    public class PickingService : IPickingService
    {
        private readonly WarehouseDbContext _context;

        public PickingService(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreatePickingAsync(string warehouseName,PickingDTO dto)
        {
            if (dto == null || warehouseName == null) throw new AppException("Invalid data.", HttpStatusCode.BadRequest);

            var warehouse = await _context.Warehouses.FirstOrDefaultAsync(w => w.Name.ToLower() == warehouseName.ToLower());

            if (warehouse == null) throw new AppException("Warehouse not found.", HttpStatusCode.NotFound);

            if (dto.PickingDate.Date <= DateTime.Today) throw new AppException("Please enter a valid date.", HttpStatusCode.BadRequest);

            var picking = new Picking
            {
                WarehouseId = warehouse.Id,
                Description = dto.Description,
                PickingDate = dto.PickingDate,
                Status = PickingStatus.Created,
            };

            await _context.Pickings.AddAsync(picking);
            await _context.SaveChangesAsync();

            return picking.Id;
        }

        public async Task<List<ShowPickingDTO>> GetAllPickingAsync()
        {
            return await _context.Pickings
                                 .Include(P => P.Warehouse)
                                 .Select(P => new ShowPickingDTO
                                 {
                                     Id = P.Id,
                                     Description = P.Description,
                                     PickingDate = P.PickingDate,
                                     Status = P.Status.ToString(),
                                     SourceWarehouseName = P.Warehouse.Name
                                 }).ToListAsync();
        }
    }
}
