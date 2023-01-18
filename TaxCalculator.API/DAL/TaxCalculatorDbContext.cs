using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using TaxCalculator.API.DAL.Entities;
using TaxCalculator.Common;
using TaxCalculator.Common.Interfaces;

namespace TaxCalculator.API.DAL
{
    public class TaxCalculatorDbContext : DbContext
    {

        public TaxCalculatorDbContext([NotNull] DbContextOptions<TaxCalculatorDbContext> options) : base(options)
        { }

        public DbSet<TaxPayer> TaxPayers { get; set; }

        public override int SaveChanges() => SaveChanges(true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ApplyAuditInfoRules();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyAuditInfoRules();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => SaveChangesAsync(true, cancellationToken);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaxPayer>()
                        .HasIndex(tp => tp.SSN)
                        .IsUnique();
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditInfo &&
                    (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default)
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
