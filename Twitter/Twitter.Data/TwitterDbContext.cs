namespace Twitter.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Twitter.Common;
    using Twitter.Contracts;
    using Twitter.Data.Migrations;
    using Twitter.Models;

    public class TwitterDbContext : IdentityDbContext<User>, ITwitterDbContext
    {
        public const string SqlConnectionString = "Server=.;Database=Twitter;Integrated Security=True;";

        public TwitterDbContext()
            : this("DefaultConnection")
        {
        }

        public TwitterDbContext(string connectionString = SqlConnectionString)
            : base(connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<TwitterDbContext, Configuration>());
        }

        public DbContext DbContext
        {
            get { return this; }
        }

        public override int SaveChanges()
        {
            ApplyAuditInfoRules();
            ApplyDeletableEntityRules();
            return base.SaveChanges();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public static TwitterDbContext Create()
        {
            return new TwitterDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new IsUnicodeAttributeConvention());

            base.OnModelCreating(modelBuilder);
                // Without this call EntityFramework won't be able to configure the identity model
        }

        private void ApplyAuditInfoRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in
                ChangeTracker.Entries()
                    .Where(
                        e =>
                            e.Entity is IAuditInfo &&
                            ((e.State == EntityState.Added) || (e.State == EntityState.Modified))))
            {
                var entity = (IAuditInfo) entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }

        private void ApplyDeletableEntityRules()
        {
            // Approach via @julielerman: http://bit.ly/123661P
            foreach (var entry in ChangeTracker.Entries()
                .Where(e => e.Entity is IDeletableEntity && (e.State == EntityState.Deleted)))
            {
                var entity = (IDeletableEntity) entry.Entity;

                entity.DeletedOn = DateTime.Now;
                entity.IsDeleted = true;
                entry.State = EntityState.Modified;
            }
        }
    }
}