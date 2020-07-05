using KnstArchitecture.EF.DbContexts;
using KnstArchitecture.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Toy.Models.ExampleContextModels
{
    public partial class ExampleContext : KnstMySqlDbContext
    {
        public ExampleContext(IMySqlUnitOfWork mySqlUnitOfWork) : base(mySqlUnitOfWork) { }

        public virtual DbSet<Example> Example { get; set; }

        public override void InnerOnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(DbSession.GetConnection<MySqlConnection>());
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
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.RowDatetime)
                    .HasColumnName("row_datetime")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}