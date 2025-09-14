using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseDAL.DTOs;
using WarehouseDAL.Interfaces;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxController : ControllerBase
    {
        private readonly IBoxService _boxService;

        public BoxController(IBoxService boxService)
        {
            _boxService = boxService;
        }

        [HttpPost("AssignItemsToBox")]
        public async Task<IActionResult> AssignItemsToBox(List<AssignItemsToBoxDTO> dtos)
        {
            await _boxService.AssignItemsToBox(dtos);
            return Ok("Items Assigned To Boxes Successfully");
        }

        [HttpGet("GetBoxItems")]
        public async Task<IActionResult> GetBoxItems()
        {
            var result = await _boxService.ShowBoxItems();
            return Ok(result);
        }

        [HttpPost("ConfirmBoxed/{transferId}")]
        public async Task<IActionResult> ConfirmBoxes(int transferId)
        {
            await _boxService.ConfirmBoxes(transferId);
            return Ok("Picking Status Now is Boxed");
        }
    }
}
