using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity;

namespace DamirMasic.Models
{
    public class DBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().ToTable("BlogPost");
            
            modelBuilder.Entity<User>().ToTable("Users");
        }
    }
}
