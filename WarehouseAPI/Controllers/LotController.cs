using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseDAL.DTOs;
using WarehouseDAL.Interfaces;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LotController : ControllerBase
    {
        private readonly ILotService _lotService;

        public LotController(ILotService lotService)
        {
            _lotService = lotService;
        }

        [HttpPost("AssignBoxesToLot")]
        public async Task<IActionResult> AssignBoxesToLot(AssignBoxToLotDTO dto)
        {
            var result = await _lotService.AssignBoxesToLot(dto);

            return Ok(new { LotId = result });
        }

        [HttpGet("GetAllLots")]
        public async Task<IActionResult> GetAllLots()
        {
            var result = await _lotService.GetAllLots();

            return Ok(result);
        }

        [HttpPost("AssignBoxesToLotAutomatic")]
        public async Task<IActionResult> AssignBoxesToLotAutomatic()
        {
            await _lotService.AssignBoxesToLotAutomatic();
            return Ok("Boxes Assigned To Lot Automatic");
        }

        [HttpPost("ReadyToShip/{transferId}")]
        public async Task<IActionResult> ReadyToShip(int transferId)
        {
            await _lotService.ReadyToShip(transferId);
            return Ok("Lot Status Now Is Ready To Ship");
        }

        [HttpPost("InTransit/{transferId}/{warehouseName}")]
        public async Task<IActionResult> InTransit(int transferId,string warehouseName)
        {
            await _lotService.InTransit(transferId,warehouseName);
            return Ok("Lot Status Now Is InTrasnsit");
        }

        [HttpPost("ApproveShipment/{transferId}")]
        public async Task<IActionResult> ApproveShipment(int transferId)
        {
            await _lotService.ApproveShipped(transferId);
            return Ok("Lot Status Now Is Completed");
        }
    }
}
