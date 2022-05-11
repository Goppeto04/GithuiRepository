using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer
{
    public class HobbyDbContext : DbContext
    {
        public HobbyDbContext()
        {

        }

        public HobbyDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-C95UOLC\SQLEXPRESS;Database=HobbyDB;Trusted_Connection=True");
            }
        }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
