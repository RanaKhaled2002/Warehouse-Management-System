using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseDAL.Data;
using WarehouseDAL.DTOs;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly WarehouseDbContext _context;

        public ItemController(WarehouseDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllItems")]
        public async Task<IActionResult> GetAllItems()
        {
            var result = await _context.Items
                                       .Select(i => new ItemDTO
                                        {
                                            Id = i.Id,
                                            Name = i.Name,
                                            BarCode = i.BarCode
                                       }).ToListAsync();
            return Ok(result);
        }
    }
}
