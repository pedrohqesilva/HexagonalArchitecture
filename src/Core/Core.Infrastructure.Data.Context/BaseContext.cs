using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.Infrastructure.Data.Context
{
    public abstract class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("hr_r");
            ConfigureTypes(modelBuilder);
        }

        private void ConfigureTypes(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
            {
                property.IsUnicode(true);
            }
        }
    }
}