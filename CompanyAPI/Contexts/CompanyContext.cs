using CompanyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CompanyAPI.Contexts
{
	public class CompanyContext : DbContext
	{
		private readonly string _connectionString;
		public DbSet<Employee> Employees { get; set; }
		public DbSet<Position> Positions { get; set; }
		public DbSet<EmployeePosition> EmployeePosition { get; set; }

		public CompanyContext(IConfiguration configuration)
		{
			_connectionString = configuration["DbCompany"];
			if (!Database.CanConnect()) Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Employee>()
				.HasMany(x => x.EmployeePositions).WithOne(x => x.Employee)
				.HasForeignKey(x => x.EmployeeId)
				.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<Position>()
				.HasMany(x => x.EmployeePositions).WithOne(x => x.Position)
				.HasForeignKey(x => x.PositionId)
				.OnDelete(DeleteBehavior.SetNull);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseMySQL(_connectionString);
		}
	}
}