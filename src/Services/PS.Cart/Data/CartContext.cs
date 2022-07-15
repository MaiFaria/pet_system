using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using PS.Cart.Models;

namespace PS.Cart.Data
{
    public sealed class CartContext : DbContext
    {
        public CartContext(DbContextOptions<CartContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<CartItem> CartItens { get; set; }
        public DbSet<CustomerCart> CustomerCart { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.Ignore<ValidationResult>();

            modelBuilder.Entity<CustomerCart>()
                .HasIndex(c => c.ClienteId)
                .HasName("IDX_Cliente");

            modelBuilder.Entity<CustomerCart>()
                .Ignore(c => c.Voucher)
                .OwnsOne(c => c.Voucher, v =>
                {
                    v.Property(vc => vc.Code)
                        .HasColumnName("VoucherCodigo")
                        .HasColumnType("varchar(50)");

                    v.Property(vc => vc.DiscountType)
                        .HasColumnName("TipoDesconto");

                    v.Property(vc => vc.Percent)
                        .HasColumnName("Percentual");

                    v.Property(vc => vc.DiscountValue)
                        .HasColumnName("ValorDesconto");
                });

            modelBuilder.Entity<CustomerCart>()
                .HasMany(c => c.Itens)
                .WithOne(i => i.CustomerCart)
                .HasForeignKey(c => c.CartId);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.Cascade;
        }
    }
}