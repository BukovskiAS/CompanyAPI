using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CompanyAPI.Models
{
	public class Position
	{
		[Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [JsonIgnore]
		public int Id { get; set; }

		[Required(ErrorMessage = "Please enter name"), MinLength(1)]
		public string Name { get; set; }

		[Range(1, 15, ErrorMessage = "Please enter correct value")]
		public int Grade { get; set; }

		[JsonIgnore]
		public virtual ICollection<EmployeePosition> EmployeePositions { get; set; } = new List<EmployeePosition>();
	}
}