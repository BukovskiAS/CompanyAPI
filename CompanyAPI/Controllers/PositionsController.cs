using CompanyAPI.Contexts;
using CompanyAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Controllers
{
	[ApiController] [Route("[controller]")]
	public class PositionsController : ControllerBase
	{
		private readonly CompanyContext _db;

		public PositionsController(CompanyContext context) => _db = context;

		[HttpGet] 
		public async Task<IActionResult> Get()
		{
			var obj = await _db.Positions.ToListAsync();
			return Ok(obj);
		}

		[HttpGet("{id}")] 
		public async Task<IActionResult> Get(int id)
		{
			var obj = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
			return Ok(obj);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] Position model)
		{
			await _db.AddAsync(model);
			await _db.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] Position model)
		{
			var obj = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
			obj.Name = model.Name; obj.Grade = model.Grade;
			await _db.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			if (_db.EmployeePosition.Any(x => x.PositionId == id))
			{
				return Problem();
			}
			var obj = await _db.Positions.FirstOrDefaultAsync(x => x.Id == id);
			_db.Positions.Remove(obj);
			await _db.SaveChangesAsync();
			return Ok();
		}
	}
}