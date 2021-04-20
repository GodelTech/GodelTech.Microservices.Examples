using Microservice.DataAccessEFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.DataAccessEFCore
{
    public class NicknamesContext : DbContext
    {
        public NicknamesContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Nickname> Nicknames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "User" }
            );
        }
    }
}
