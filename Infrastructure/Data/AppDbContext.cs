using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
	public class AppDbContext: DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
		{
		}

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Password).IsRequired();
        }

    }
}

