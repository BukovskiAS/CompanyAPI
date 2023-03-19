using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CompanyAPI.Models
{
	public class Employee
	{
		[Key] [ScaffoldColumn(false)] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [JsonIgnore]
		public int Id { get; set; } 

		[Required(ErrorMessage = "Please enter name"), MinLength(1)]
		public string FIO { get; set; }

		[DisplayFormat(DataFormatString = "{dd.MM.yyyy}")]
		public DateTime DateOfBirth { get; set; }

		public virtual ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
	}
}