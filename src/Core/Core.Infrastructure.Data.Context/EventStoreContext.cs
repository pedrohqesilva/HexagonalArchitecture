using Core.Domain.Events;
using Core.Infrastructure.Data.Context.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Data.Context
{
    public class EventStoreContext : BaseContext
    {
        public EventStoreContext(DbContextOptions options)
           : base(options)
        {
        }

        public DbSet<EventLog> EventLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EventLogMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}