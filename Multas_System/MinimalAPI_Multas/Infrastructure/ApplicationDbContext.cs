using Microsoft.EntityFrameworkCore;
using MinimalAPI_Multas.Models.ApplicationModel;
using System.Reflection;

namespace MinimalAPI_Multas.Infrastructure
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }
        protected ApplicationDbContext()
        {

        }
        public DbSet<MultaModel>? Multa { get; set; } = null!;
        public DbSet<PrecioModel>? Precio { get; set; } = null!;

        //public ApplicationDbContext()
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("No connection string set for database.");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MultaModel>(entity =>
            {
                entity.HasKey(e => e.IdMulta);
                entity.Property(e => e.Patente).HasMaxLength(10).HasColumnName("Patente").IsRequired();
                entity.Property(e => e.Active).HasDefaultValue(true).IsRequired();
                entity.Property(e => e.Fecha).HasDefaultValueSql("getdate()").IsRequired();
                entity.Property(e => e.Monto).HasMaxLength(10).HasColumnName("Monto").IsRequired();
            });

            modelBuilder.Entity<PrecioModel>(entity =>
            {
                entity.HasKey(e => e.idPrecio);
                entity.Property(e => e.Monto).HasColumnName("Monto").IsRequired();
            });


            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }



    }
}
