using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PS.Client.API.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Models.Address>
    {
        public void Configure(EntityTypeBuilder<Models.Address> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.PublicPlace)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(c => c.Cep)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(c => c.Complement)
                .HasColumnType("varchar(250)");

            builder.Property(c => c.District)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.City)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.State)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.ToTable("Enderecos");
        }
    }
}
