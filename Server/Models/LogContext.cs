using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class BloggingContext : DbContext
    {
        public DbSet<AcessLog> Logs { get; set; }
        #region Required
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcessLog>()
                .Property(b => b.Number)
                .IsRequired();
        }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Data Source=tcp:., 1433;Initial Catalog=SquidProject;User ID=SA;Password=p@ssw0rd_sql");
        }
    }

    public class AcessLog
    {
        [Key]
        public string Number { get; set; }
        public string RandomNumber { get; set; }
        public string Ipv6 { get; set; }
        public string Protocol { get; set; }
        public string Port { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Test { get; set; }
        public string Type { get; set; }
    }
}
