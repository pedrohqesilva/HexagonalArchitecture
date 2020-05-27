using ConsoleAppRE;
using Core.Infrastructure.Data.Context;
using Example.Infrastructure.Data.Context.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Example.Infrastructure.Data.Context
{
    public class ExampleContext : BaseContext
    {
        public ExampleContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Regions> Regions { get; private set; }
        public DbSet<Countries> Countries { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RegionMap());
            modelBuilder.ApplyConfiguration(new CountryMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}