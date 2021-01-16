using LaundryIroningCommon;
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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
                                  
                                             
        }
       
       
    }
}


