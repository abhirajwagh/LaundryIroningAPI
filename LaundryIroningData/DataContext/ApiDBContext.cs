using LaundryIroningCommon;
using LaundryIroningEntity.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System;

namespace LaundryIroningData.DataContext
{
    public partial class ApiDBContext : DbContext
    {
        IBuildConnectionString _buildConnectionString;

        public ApiDBContext(DbContextOptions<ApiDBContext> options, IBuildConnectionString buildConnectionString) : base(options)
        {
            this._buildConnectionString = buildConnectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                string connectionString = _buildConnectionString.PreparedConnection();
                optionsBuilder.UseSqlServer(connectionString);
            
        }

        public ApiDBContext()
        { }

        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserId").HasDefaultValueSql("(newid())");
                entity.HasKey(e => e.UserId);
            });

        }
       
       
    }
}


