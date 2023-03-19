using CompanyAPI.Contexts;
using CompanyAPI.Models;
using CompanyAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Controllers
{
	[ApiController] [Route("[controller]")]
	public class EmployeesController : ControllerBase
	{
		private readonly CompanyContext _db;

		public EmployeesController(CompanyContext context) => _db = context;

		[HttpGet]
		public async Task<IActionResult> Get()
		{
			var obj = await _db.Employees.Include(x => x.EmployeePositions)
				.ThenInclude(y => y.Position).ToListAsync();
			return Ok(obj);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(int id)
		{
			var obj = await _db.Employees.Include(x => x.EmployeePositions)
				.ThenInclude(y => y.Position).FirstOrDefaultAsync(x => x.Id == id);
			return Ok(obj);
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] EmployeePositionViewModel vm)
		{
			var obj = new Employee()
			{
				FIO = vm.FIO,
				DateOfBirth = vm.DateOfBirth
			};
			if (vm.PositionIds is not null)
			{
				foreach (var item in vm.PositionIds)
				{
					var temp = new EmployeePosition()
					{
						Employee = obj, 
						PositionId = item
					};
					obj.EmployeePositions.Add(temp);
				}
			}
			await _db.Employees.AddAsync(obj);
			await _db.SaveChangesAsync();
			return Ok();
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromBody] EmployeePositionViewModel vm)
		{
			var obj = await _db.Employees
				.Include(x => x.EmployeePositions)
				.ThenInclude(y => y.Position)
				.FirstOrDefaultAsync(x => x.Id == id);
			obj.FIO = vm.FIO; obj.DateOfBirth = vm.DateOfBirth;
			var existingIds = obj.EmployeePositions.Select(x => x.Id).ToList();
			var selectedIds = vm.PositionIds.ToList();
			var toAdd = selectedIds.Except(existingIds).ToList();
			var toRemove = existingIds.Except(selectedIds).ToList();
			obj.EmployeePositions = obj.EmployeePositions.Where(x => !toRemove.Contains((int)x.PositionId)).ToList();
			foreach (var item in toAdd)
			{
				obj.EmployeePositions.Add(new EmployeePosition()
				{
					PositionId = item
				});
			}
			_db.Employees.Update(obj);

			await _db.SaveChangesAsync();
			return Ok();
		}

		[HttpDelete]
		public async Task<IActionResult> Delete(int id)
		{
			var obj = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
			_db.Employees.Remove(obj);
			await _db.SaveChangesAsync();
			return Ok();
		}
	}
}