namespace Twitter.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using Twitter.Models;

    public interface ITwitterDbContext
    {
        IDbSet<User> Users { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}