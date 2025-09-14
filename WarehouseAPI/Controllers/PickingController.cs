using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WarehouseBLL.Interfaces;
using WarehouseDAL.DTOs;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickingController : ControllerBase
    {
        private readonly IPickingService _pickingService;

        public PickingController(IPickingService pickingService)
        {
            _pickingService = pickingService;
        }

        [HttpPost("CraetePicking/{warehouse}")]
        public async Task<IActionResult> CreatePickingAsync(string warehouse,[FromBody]PickingDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values
                                                                 .SelectMany(v => v.Errors)
                                                                 .Select(e => e.ErrorMessage)
                                                                 .ToList());

            var result = await _pickingService.CreatePickingAsync(warehouse, dto);

            return Ok(new { PickingId = result });
        }

        [HttpGet("GetAllPicking")]
        public async Task<IActionResult> GetAllPickingsAsync()
        {
            var result = await _pickingService.GetAllPickingAsync();
            return Ok(result);
        }
    }
}
