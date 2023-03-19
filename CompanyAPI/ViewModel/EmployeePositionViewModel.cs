using System;

namespace CompanyAPI.ViewModel
{
	public class EmployeePositionViewModel
    {
		public string FIO { get; set; }
		public DateTime DateOfBirth { get; set; }
		public int[] PositionIds { get; set; }
	}
}
