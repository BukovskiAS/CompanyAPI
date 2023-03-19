using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CompanyAPI.Models
{
	public class EmployeePosition
	{
		[Key] [DatabaseGenerated(DatabaseGeneratedOption.Identity)] [JsonIgnore]
		public int Id { get; set; }

		[ForeignKey("EmployeeId")] [JsonIgnore]
		public int? EmployeeId { get; set; }

		[ForeignKey("PositionId")] [JsonIgnore]
		public int? PositionId { get; set; }

		[JsonIgnore]
		public virtual Employee Employee { get; set; } 
		public virtual Position Position { get; set; }
	}
}