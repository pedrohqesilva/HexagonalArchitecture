using ConsoleAppRE;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Example.Infrastructure.Data.Context.Mappings
{
    internal class RegionMap : IEntityTypeConfiguration<Regions>
    {
        public void Configure(EntityTypeBuilder<Regions> builder)
        {
            builder.HasKey(e => e.RegionId)
                .HasName("REG_ID_PK");

            builder.ToTable("REGIONS");

            builder.HasIndex(e => e.RegionId)
                .HasName("REG_ID_PKX")
                .IsUnique();

            builder.Property(e => e.RegionId)
                .HasColumnName("REGION_ID")
                .HasColumnType("NUMBER");

            builder.Property(e => e.RegionName)
                .HasColumnName("REGION_NAME")
                .HasColumnType("VARCHAR2(25)");
        }
    }
}