using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VinDemo.Domain.Entities;
using VinDemo.Domain.Interfaces;

namespace VinDemo.DataAccess.SqlServer.Context
{
    public class VinDemoDbContext : DbContext, IVinDemoDbContext
    {
        public VinDemoDbContext() : base("VinDemoConnectionString")
        {
        }

        public DbSet<Member> Members { get; set; }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return base.SaveChangesAsync();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        private void AddTimestamps()
        {
            var createdEntities = ChangeTracker.Entries()
                .Where(x => x.Entity is ITrackCreatedDate && x.State == EntityState.Added);
            foreach (var entity in createdEntities)
            {
                ((ITrackCreatedDate) entity.Entity).CreatedDate = DateTime.UtcNow;
            }

            var modifiedEntities = ChangeTracker.Entries().Where(x =>
                x.Entity is ITrackModifiedDate && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in modifiedEntities)
            {
                ((ITrackModifiedDate) entity.Entity).ModifiedDate = DateTime.UtcNow;
            }
        }
    }
}