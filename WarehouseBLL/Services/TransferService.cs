using Microsoft.EntityFrameworkCore;
using System.Net;
using WarehouseBLL.Exceptions;
using WarehouseDAL.Data;
using WarehouseDAL.DTOs;
using WarehouseDAL.Interfaces;
using WarehouseDAL.Models;

namespace WarehouseBLL.Services
{
    public class TransferService : ITransferService
    {
        private readonly WarehouseDbContext _context;

        public TransferService(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateTransferAsync(TransferDTO dto)
        {
            if(dto == null) throw new AppException("Invalid data.", HttpStatusCode.BadRequest);

            var picking = await _context.Pickings.FindAsync(dto.PickingId);

            if (picking == null) throw new AppException("Picking Not Found", HttpStatusCode.NotFound);

            var destination = await _context.Warehouses.FirstOrDefaultAsync(W => W.Name.ToLower() == dto.DestinationWarehouseName.ToLower());

            if (destination == null) throw new AppException("Warehouse not found", HttpStatusCode.NotFound);

            var transfer = new Transfer
            {
                Status = TransferStatus.OnHold,
                PickingId = dto.PickingId,
                SourceWarehouseId = picking.WarehouseId,
                DestinationWarehouseId = destination.Id
            };

            await _context.Transfers.AddAsync(transfer);
            await _context.SaveChangesAsync();

            return transfer.Id;
        }

        public async Task<List<ShowTransferDTO>> GetAllTransfer()
        {
            return await _context.Transfers.Include(T => T.Picking)
                                           .Select(T => new ShowTransferDTO
                                           {
                                               Id = T.Id,
                                               PickingId = T.PickingId,
                                               SourceWarehouseName = T.SourceWarehouse.Name,
                                               DestinationWarehouseName = T.DestinationWarehouse.Name,
                                               Status = T.Status.ToString()
                                           }).ToListAsync();
        }

        public async Task<List<ShowTransferDTO>> GetTransferWithOnholdStatusAsync(string status,string warehouseName)
        {
            if (status == null || warehouseName == null) throw new AppException("Invalid Data", HttpStatusCode.BadRequest);

            var warehouse = await _context.Warehouses.FirstOrDefaultAsync(W => W.Name == warehouseName);

            if (warehouse == null) throw new AppException("Warehouse not found", HttpStatusCode.NotFound);

            return await _context.Transfers
                           .Include(W => W.DestinationWarehouse)
                           .Include(W => W.SourceWarehouse)
                           .Include(P => P.Picking)
                           .Where(T=> T.Status.ToString().ToLower() == status.ToLower() && T.SourceWarehouse.Name.ToLower() == warehouseName.ToLower())
                           .Select(T => new ShowTransferDTO
                           {
                               Id = T.Id,
                               PickingId = T.PickingId,
                               SourceWarehouseName = T.SourceWarehouse.Name,
                               DestinationWarehouseName = T.DestinationWarehouse.Name,
                               Status = T.Status.ToString()
                           }).ToListAsync();
            
        }

        public async Task<List<ShowTransferDTO>> GetTransferWithSpecificPickingStatusAsync(string status)
        {
            if (status == null) throw new AppException("Invalid Data", HttpStatusCode.BadRequest);

            return await _context.Transfers
                           .Include(W => W.DestinationWarehouse)
                           .Include(W => W.SourceWarehouse)
                           .Include(P => P.Picking)
                           .Where(T => T.Picking.Status.ToString().ToLower() == status.ToLower())
                           .Select(T => new ShowTransferDTO
                           {
                               Id = T.Id,
                               PickingId = T.PickingId,
                               SourceWarehouseName = T.SourceWarehouse.Name,
                               DestinationWarehouseName = T.DestinationWarehouse.Name,
                               Status = T.Status.ToString()
                           }).ToListAsync();

        }

        public async Task AssignItemsToTransfer(List<AssignItemsToTransfer> dtos)
        {
            foreach(var dto in dtos)
            {
                var transfer = await _context.Transfers.FindAsync(dto.TransferId);

                if (transfer == null) throw new AppException("Transfer Not found");

                var item = await _context.Items.FindAsync(dto.ItemId);

                if (item == null) throw new AppException("Item Not found");

                var exist = await _context.TransferItems.AnyAsync(
                    TI => TI.TransferId == dto.TransferId && TI.ItemId == dto.ItemId);

                if(!exist)
                {
                    var trsnsferItem = new TransferItem
                    {
                        TransferId = dto.TransferId,
                        ItemId = dto.ItemId,
                        RequestedQty = dto.QuantityRequired,
                        ScannedQty = 0,
                    };

                    _context.TransferItems.Add(trsnsferItem);
                }

            }
             await _context.SaveChangesAsync();
        }

        public async Task<List<TransferItemDTO>> GetTransferItemsAsync(int transferId)
        {
            var transfer = await _context.Transfers.FindAsync(transferId);

            if (transfer == null) throw new AppException("Transfer not found", HttpStatusCode.NotFound);

            return await _context.TransferItems
                           .Include(TI => TI.Item)
                           .Where(TI => TI.TransferId == transferId)
                           .Select(TI => new TransferItemDTO
                           {
                               ItemId = TI.ItemId,
                               ItemName = TI.Item.Name,
                               Barcode = TI.Item.BarCode,
                               QuantityRequired = TI.RequestedQty,
                               QuantityScanned = TI.ScannedQty
                           }).ToListAsync();
        }

        public async Task ScanItemAsync(ScanItemsDTO dto)
        {
            var transfer = await _context.Transfers.FindAsync(dto.TransferId);

            if (transfer == null) throw new AppException("Transfer not found", HttpStatusCode.NotFound);

            var item = await _context.Items.FirstOrDefaultAsync(I => I.BarCode == dto.BarCode);

            if(item == null) throw new AppException("Item not found", HttpStatusCode.NotFound);

            var transferItem = await _context.TransferItems.FirstOrDefaultAsync(TI => TI.Item.BarCode == dto.BarCode && TI.TransferId == dto.TransferId);

            if(transferItem == null) throw new AppException("Item not assigned to this transfer", HttpStatusCode.BadRequest);

            if(transferItem.ScannedQty >= transferItem.RequestedQty) throw new AppException("Required quantity already scanned", HttpStatusCode.BadRequest);

            transferItem.ScannedQty += 1;
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmTransferAsync(int transferId)
        {
            var transfer = await _context.Transfers.FindAsync(transferId);

            if (transfer == null) throw new AppException("Transfer not found", HttpStatusCode.NotFound);

            var transferItems = await _context.TransferItems.Where(TI => TI.TransferId == transferId).ToListAsync();

            if (transferItems == null) throw new AppException("No items for this transfer", HttpStatusCode.BadRequest);

            if(transferItems.Any(TI=>TI.ScannedQty < TI.RequestedQty))
                throw new AppException("Not all items scanned completely", HttpStatusCode.BadRequest);

            transfer.Status = TransferStatus.Processed;
            await _context.SaveChangesAsync();
        }

        public async Task ConfirmSorted(int transferId)
        {
            var transfer = await _context.Transfers.FindAsync(transferId);

            if (transfer == null) throw new AppException("Transfer not found", HttpStatusCode.NotFound);

            if (transfer.Status.ToString().ToLower() == TransferStatus.Processed.ToString().ToLower())
            {
                var picking = await _context.Pickings.FindAsync(transfer.PickingId);

                if (picking == null) throw new AppException("Picking NotFound", HttpStatusCode.NotFound);

                picking.Status = PickingStatus.Sorted;
                await _context.SaveChangesAsync();
            }
            else throw new AppException("Transfer must be processed before confirming sorted");

            
        }
    }
}
