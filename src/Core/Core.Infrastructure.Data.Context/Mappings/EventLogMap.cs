using Core.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Core.Infrastructure.Data.Context.Mappings
{
    internal class EventLogMap : IEntityTypeConfiguration<EventLog>
    {
        public void Configure(EntityTypeBuilder<EventLog> builder)
        {
            builder.ToTable("LOG_EVENTO_DOMINIO");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .HasMaxLength(32)
                   .HasColumnName("COD_EVENTO_LOG");

            builder.Property(x => x.Content)
                   .HasColumnName("CONTEUDO_EVENTO")
                   .HasMaxLength(4000)
                   .IsRequired();

            builder.Property(x => x.CreationDate)
                   .HasColumnName("DAT_EVENTO")
                   .IsRequired();

            builder.Property(x => x.EventTypeName)
                  .HasColumnName("NOME_TIPO_EVENTO")
                  .HasMaxLength(150)
                  .IsRequired();

            builder.Property(x => x.State)
                  .HasColumnName("ESTADO_EVENTO")
                  .IsRequired();
        }
    }
}