using ConsoleAppRE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Infrastructure.Data.Context.Mappings
{
    internal class CountryMap : IEntityTypeConfiguration<Countries>
    {
        public void Configure(EntityTypeBuilder<Countries> builder)
        {
            builder.HasKey(e => e.CountryId)
                .HasName("COUNTRY_C_ID_PK");

            builder.ToTable("COUNTRIES");

            builder.HasIndex(e => e.CountryId)
                .HasName("COUNTRY_C_ID_PKX")
                .IsUnique();

            builder.Property(e => e.CountryId)
                .HasColumnName("COUNTRY_ID")
                .HasColumnType("CHAR(2)")
                .ValueGeneratedNever();

            builder.Property(e => e.CountryName)
                .HasColumnName("COUNTRY_NAME")
                .HasColumnType("VARCHAR2(40)");

            builder.Property(e => e.RegionId)
                .HasColumnName("REGION_ID")
                .HasColumnType("NUMBER");

            builder.HasOne(d => d.Region)
                .WithMany(p => p.Countries)
                .HasForeignKey(d => d.RegionId)
                .HasConstraintName("COUNTR_REG_FK");
        }
    }
}