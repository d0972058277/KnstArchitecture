using System;
using System.Data.Common;
using KnstArchitecture.EF.DbContexts;
using KnstArchitecture.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace KnstApiSqlServer.Models.Test
{
    public partial class TestContext : KnstDbContext
    {
        private readonly IServiceProvider _serviceProvider;
        public TestContext(IEFCoreUnitOfWork efUnitOfWork, IServiceProvider serviceProvider) : base(efUnitOfWork)
        {
            _serviceProvider = serviceProvider;
        }

        public virtual DbSet<Example> Example { get; set; }

        public override void InnerOnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var loggerFactory = _serviceProvider.GetRequiredService<ILoggerFactory>();
            optionsBuilder.UseSqlServer(DbSession.GetConnection<DbConnection>()).UseLoggerFactory(loggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Test");
            modelBuilder.Entity<Example>(entity =>
            {
                entity.ToTable("example");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IsDelete).HasColumnName("is_delete");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)");

                entity.Property(e => e.RowDatetime)
                    .HasColumnName("row_datetime")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}