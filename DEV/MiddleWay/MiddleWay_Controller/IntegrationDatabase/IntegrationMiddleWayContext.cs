using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWay_Controller.IntegrationDatabase
{
    public class IntegrationMiddleWayContext : DbContext
    {
        public virtual DbSet<Configurations> Configurations { get; set; }
        public virtual DbSet<Mappings> Mappings { get; set; }
        public virtual DbSet<ProcessErrors> ProcessErrors { get; set; }
        public virtual DbSet<Processes> Processes { get; set; }
        public virtual DbSet<Transformations> Transformations { get; set; }
        public virtual DbSet<TABLE> TABLE { get; set; }
        public virtual DbSet<TABLE> TABLE { get; set; }
        public virtual DbSet<TABLE> TABLE { get; set; }
        public virtual DbSet<TABLE> TABLE { get; set; }

        public IntegrationMiddleWayContext(DbContextOptions<IntegrationMiddleWayContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TABLE>(entity =>
            {
                //entity.HasKey(e => e.Host)
                //    .HasName("PK_HostConnections");

                //entity.Property(e => e.Host)
                //    .HasColumnName("host")
                //    .HasColumnType("varchar(250)");

                //entity.Property(e => e.Connection)
                //    .IsRequired()
                //    .HasColumnType("varchar(1000)");

                //entity.Property(e => e.CreateDate)
                //    .HasColumnType("datetime")
                //    .HasDefaultValueSql("getutcdate()");

                //entity.Property(e => e.Enabled).HasDefaultValueSql("1");
            });
        }
    }
}
