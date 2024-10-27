using Microsoft.EntityFrameworkCore;

namespace WebApp1.Models.DataBase
{
	public class DataBase : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Tacket> Tackets { get; set; }
		public DbSet<DetailsTaskUser> DetailsUser { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Data Source=MOHAMEDYOUSRY\\SQLEXPRESS;Initial Catalog=ITI;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
		}

	}
}
