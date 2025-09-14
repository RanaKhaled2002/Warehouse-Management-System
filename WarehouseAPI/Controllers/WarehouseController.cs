using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WarehouseDAL.Data;

namespace WarehouseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseDbContext _context;

        public WarehouseController(WarehouseDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllWarehouses")]
        public async Task<IActionResult> GetAllWarehouses()
        {
            return Ok(await _context.Warehouses.ToListAsync());
        }
    }
}
