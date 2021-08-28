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

        public virtual DbSet<UserTypes> UserTypes { get; set; }
        public virtual DbSet<IroningOrder> IroningOrder { get; set; }
        public virtual DbSet<LaundryOrder> LaundryOrder { get; set; }
        public virtual DbSet<IroningLaundryOrder> IroningLaundryOrder { get; set; }

        public virtual DbSet<OrderAgentMapping> OrderAgentMapping { get; set; }

        public virtual DbSet<PromoCodes> PromoCodes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserId").HasDefaultValueSql("(newid())");
                entity.HasKey(e => e.UserId);
            });

            modelBuilder.Entity<IroningOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<LaundryOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<IroningLaundryOrder>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<UserTypes>(entity =>
            {
                entity.Property(e => e.UserTypeId).HasColumnName("UserTypeId");
                entity.HasKey(e => e.UserTypeId);
            });

            modelBuilder.Entity<OrderAgentMapping>(entity =>
            {
                entity.Property(e => e.OrderMappingId).HasColumnName("OrderMappingId");
                entity.HasKey(e => e.OrderMappingId);
            });

            modelBuilder.Entity<PromoCodes>(entity =>
            {
                entity.Property(e => e.PromoCodeId).HasColumnName("PromoCodeId").HasDefaultValueSql("(newid())");
                entity.HasKey(e => e.PromoCodeId);
            });
        }


    }
}


