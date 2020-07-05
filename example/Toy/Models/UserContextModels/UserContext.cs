using KnstArchitecture.EF.DbContexts;
using KnstArchitecture.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace Toy.Models.UserContextModels
{
    public partial class UserContext : KnstMySqlDbContext
    {
        public UserContext(IMySqlUnitOfWork unitOfWork) : base(unitOfWork) { }

        public virtual DbSet<User> User { get; set; }

        public override void InnerOnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(DbSession.GetConnection<MySqlConnection>());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Test");
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasColumnType("varchar(45)")
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.RowDatetime)
                    .HasColumnName("row_datetime")
                    .HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}