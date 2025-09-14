using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseDAL.DTOs;
using WarehouseDAL.Interfaces;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : ControllerBase
    {
        private readonly ITransferService _transferService;

        public TransferController(ITransferService transferService)
        {
            _transferService = transferService;
        }

        [HttpPost("CreateTransfer")]
        public async Task<IActionResult> CreateTransfer(TransferDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values.SelectMany(V => V.Errors).Select(E => E.ErrorMessage).ToList());

            var result = await _transferService.CreateTransferAsync(dto);

            return Ok(new { TrasferId = result });
        }

        [HttpGet("GetAllTransfers")]
        public async Task<IActionResult> GetAllTransfers()
        {
            var result = await _transferService.GetAllTransfer();
            return Ok(result);
        }

        [HttpGet("GetOnHoldTransfer/{status}/{warehouseName}")]
        public async Task<IActionResult> GetTransfer(string status,string warehouseName)
        {
            var result = await _transferService.GetTransferWithOnholdStatusAsync(status,warehouseName);
            return Ok(result);
        }

        [HttpGet("GetTransferWithSpecificPickingStatus/{status}")]
        public async Task<IActionResult> GetTransferWithSpecificPickingStatus(string status)
        {
            var result = await _transferService.GetTransferWithSpecificPickingStatusAsync(status);
            return Ok(result);
        }

        [HttpPost("AssignItemsToTransfer")]
        public async Task<IActionResult> AssignItemsToTransfer(List<AssignItemsToTransfer> dtos)
        {
            await _transferService.AssignItemsToTransfer(dtos);

            return Ok("Items Added To Transfer");
        }

        [HttpGet("GetTransferItems/{transferId}")]
        public async Task<IActionResult> GetTransferItems(int transferId)
        {
            var result = await _transferService.GetTransferItemsAsync(transferId);

            return Ok(result);
        }

        [HttpPost("ScanItem")]
        public async Task<IActionResult> ScanItem(ScanItemsDTO dto)
        {
            await _transferService.ScanItemAsync(dto);
            return Ok("Item scanned successfully.");
        }

        [HttpPost("ConfirmTransfer/{transferId}")]
        public async Task<IActionResult> ConfirmTransfer(int transferId)
        {
            await _transferService.ConfirmTransferAsync(transferId);
            return Ok("Transfer processed successfully.");
        }

        [HttpPost("ChangePickingToSorted/{transferId}")]
        public async Task<IActionResult> ConfirmPicking(int transferId)
        {
            await _transferService.ConfirmSorted(transferId);
            return Ok("Picking status now is processed");
        }
    }
}
