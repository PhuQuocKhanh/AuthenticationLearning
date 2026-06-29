using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JWTAuthDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected AppDbContext()
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasMany(x => x.RefreshTokens)
                        .WithOne()
                        .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Username="khanh",
                    Email="khanh@gmail.com",
                    PasswordHash="123456",
                    Role="Admin",
                    IsActive=true
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}