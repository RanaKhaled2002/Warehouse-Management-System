using Microsoft.EntityFrameworkCore;
using System.Net;
using WarehouseBLL.Exceptions;
using WarehouseDAL.Data;
using WarehouseDAL.DTOs;
using WarehouseDAL.Interfaces;
using WarehouseDAL.Models;

namespace WarehouseBLL.Services
{
    public class BoxService : IBoxService
    {
        private readonly WarehouseDbContext _context;

        public BoxService(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task AssignItemsToBox(List<AssignItemsToBoxDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var transfer = await _context.Transfers.FindAsync(dto.TransferId);

                if (transfer == null)
                    throw new AppException("Transfer Not Found", HttpStatusCode.NotFound);

                if (transfer.Status.ToString().ToLower() != TransferStatus.Processed.ToString().ToLower())
                    throw new AppException("Transfer Status must be processed", HttpStatusCode.BadRequest);

                foreach (var item in dto.Items)
                {
                    var transferItems = await _context.TransferItems
                        .FirstOrDefaultAsync(TI => TI.TransferId == dto.TransferId && TI.ItemId == item.ItemId);

                    if (transferItems == null)
                        throw new AppException("No items for this transfer", HttpStatusCode.NotFound);

                    var totalAssignedToBoxes = await _context.BoxItems
                        .Where(BI => BI.ItemId == item.ItemId && BI.Box.TransferId == dto.TransferId)
                        .SumAsync(BI => (int?)BI.Quantity) ?? 0;

                    if (totalAssignedToBoxes + item.Quantity > transferItems.ScannedQty)
                        throw new AppException(
                            $"Can't assign {item.Quantity} of item {item.ItemId}. Already assigned {totalAssignedToBoxes}, scanned: {transferItems.ScannedQty}",
                            HttpStatusCode.BadRequest);
                }

                var box = new Box
                {
                    TransferId = dto.TransferId,
                    Label = $"Box-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}"
                };

                _context.Boxes.Add(box);
                await _context.SaveChangesAsync();

                foreach (var item in dto.Items)
                {
                    var boxItem = new BoxItem
                    {
                        BoxId = box.Id,
                        ItemId = item.ItemId,
                        Quantity = item.Quantity
                    };

                    _context.BoxItems.Add(boxItem);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ShowBoxItemsDTO>> ShowBoxItems()
        {
            return await _context.BoxItems.Include(BI => BI.Item).ThenInclude(BI => BI.TransferItems)
                             .Select(BI => new ShowBoxItemsDTO
                             {
                                 Id = BI.Id,
                                 BoxId = BI.BoxId,
                                 ItemId = BI.ItemId,
                                 Quantity = BI.Quantity,
                             }).ToListAsync();
        }

        public async Task ConfirmBoxes(int transferId)
        {
            var transfer = await _context.Transfers
                                   .Include(T => T.Picking)
                                   .Include(T => T.TransferItems)
                                   .ThenInclude(T => T.Item)
                                   .FirstOrDefaultAsync(T => T.Id == transferId);

            if (transfer == null) throw new AppException("Transfer notfound", HttpStatusCode.NotFound);

            var totalscanned = transfer.TransferItems.Sum(TI => TI.ScannedQty);

            var totalBoxed = await _context.BoxItems
                                     .Where(BI => BI.Box.TransferId == transferId)
                                     .SumAsync(BI => BI.Quantity);

            if (totalBoxed != totalscanned) throw new AppException("Not all items have been boxed",HttpStatusCode.BadRequest);

            transfer.Picking.Status = PickingStatus.Boxed;
            await _context.SaveChangesAsync();
        }
    }
}
