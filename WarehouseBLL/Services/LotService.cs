using Microsoft.EntityFrameworkCore;
using System.Net;
using WarehouseBLL.Exceptions;
using WarehouseDAL.Data;
using WarehouseDAL.DTOs;
using WarehouseDAL.Interfaces;
using WarehouseDAL.Models;

namespace WarehouseBLL.Services
{
    public class LotService : ILotService
    {
        private readonly WarehouseDbContext _context;

        public LotService(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<int> AssignBoxesToLot(AssignBoxToLotDTO dto)
        {
            var boxes = await _context.Boxes.Where(B => dto.BoxeIds.Contains(B.Id)).ToListAsync();

            if (boxes.Count == 0) throw new AppException("No boxes found", HttpStatusCode.NotFound);

            var lot = new Lot
            {
                LotCode = $"LOT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                LotStatus = LotStatus.Open
            };

            _context.Lots.Add(lot);
            await _context.SaveChangesAsync();

            foreach(var box in boxes)
            {
                box.LotId = lot.Id;
            }

            lot.LotStatus = LotStatus.Close;

            await _context.SaveChangesAsync();

            return lot.Id;
        }

        public async Task<List<LotDTO>> GetAllLots()
        {
           return await  _context.Lots
                    .Include(L => L.Boxes)
                    .Select(L => new LotDTO
                    {
                        LotId = L.Id,
                        Label = L.LotCode,
                        LotStatus = L.LotStatus.ToString(),
                        ShipmentStatus = L.ShipmentStatus.ToString(),
                        BoxIds = L.Boxes.Select(B=>B.Id).ToList()
                    }).ToListAsync();
        }

        public async Task AssignBoxesToLotAutomatic()
        {
            var boxes = await _context.Boxes.Where(L => L.LotId == null).ToListAsync();

            if (boxes.Count == 0) return;

            var lot = new Lot
            {
                LotCode = $"LOT-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}",
                LotStatus = LotStatus.Open
            };

            _context.Lots.Add(lot);
            await _context.SaveChangesAsync();

            foreach (var box in boxes)
            {
                box.LotId = lot.Id;
            }

            lot.LotStatus = LotStatus.Close;

            await _context.SaveChangesAsync();
        }

        public async Task ReadyToShip(int transferId)
        {
            var boxes = await _context.Boxes.Include(B=>B.Transfer).Where(B=>B.TransferId == transferId).ToListAsync();

            if (boxes == null || boxes.Count == 0) throw new AppException("No boxes for this transfer", HttpStatusCode.NotFound);

            bool allBoxesAssignedToLot = boxes.All(b => b.LotId != null);

            if (!allBoxesAssignedToLot)
                throw new AppException("Not all boxes assigned to a lot", HttpStatusCode.BadRequest);

            var lotIds = boxes.Select(B => B.LotId.Value).Distinct().ToList();

            var lots = await _context.Lots
                                     .Where(L => lotIds.Contains(L.Id))
                                     .ToListAsync();

            foreach (var lot in lots)
            {
                if(lot.LotStatus.ToString().ToLower() != LotStatus.Close.ToString().ToLower())
                {
                    throw new AppException($"LotId = {lot.Id} Not closed", HttpStatusCode.BadRequest);
                }
                lot.ShipmentStatus = ShipmentStatus.Ready;
            }

            await _context.SaveChangesAsync();
        }

        public async Task InTransit(int transferId,string destinationWarehouseName)
        {
            var boxes = await _context.Boxes
                                      .Include(b => b.Lot)
                                      .Include(b=>b.Transfer)
                                      .ThenInclude(b=>b.DestinationWarehouse)
                                      .Where(b => b.TransferId == transferId &&
                                                b.LotId != null &&
                                                b.Lot.ShipmentStatus == ShipmentStatus.Ready
                                                && b.Transfer.DestinationWarehouse.Name==destinationWarehouseName)
                                      .ToListAsync();

            if (boxes == null || boxes.Count == 0) throw new AppException("No boxes for this transfer", HttpStatusCode.NotFound);

            var lotIds = boxes.Select(b => b.LotId.Value).Distinct().ToList();

            var lots = await _context.Lots
                .Where(l => lotIds.Contains(l.Id))
                .ToListAsync();

            foreach (var lot in lots)
            {
                lot.ShipmentStatus = ShipmentStatus.InTransit;
            }

            await _context.SaveChangesAsync();
        }

        public async Task ApproveShipped(int transferId)
        {
            var boxes = await _context.Boxes
                                      .Include(b => b.Lot)
                                      .Where(b => b.TransferId == transferId &&
                                                b.LotId != null &&
                                                b.Lot.ShipmentStatus == ShipmentStatus.InTransit)
                                      .ToListAsync();

            if (boxes == null || boxes.Count == 0) throw new AppException("No boxes for this transfer", HttpStatusCode.NotFound);

            var lotIds = boxes.Select(b => b.LotId.Value).Distinct().ToList();

            var lots = await _context.Lots
                .Where(l => lotIds.Contains(l.Id))
                .ToListAsync();

            foreach (var lot in lots)
            {
                lot.ShipmentStatus = ShipmentStatus.Completed;
            }

            var transfer = await _context.Transfers.Include(T=>T.Picking).FirstOrDefaultAsync(T=>T.Id == transferId);
            transfer.Status = TransferStatus.Completed;
            transfer.Picking.Status = PickingStatus.Completed;

            await _context.SaveChangesAsync();
        }
    }
}
