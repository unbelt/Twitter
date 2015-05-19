namespace Twitter.Data
{
    using System;
    using System.Collections.Generic;

    using Twitter.Contracts;
    using Twitter.Data.Repositories;
    using Twitter.Models;

    public class TwitterData : ITwitterData
    {
        private readonly ITwitterDbContext context;
        private readonly Dictionary<Type, object> repositories = new Dictionary<Type, object>();

        public TwitterData(ITwitterDbContext context)
        {
            this.context = context;
        }

        public ITwitterDbContext Context
        {
            get { return context; }
        }

        //public IRepository<T> GetGenericRepository<T>() where T : class
        //{
        //    if (typeof(T).IsAssignableFrom(typeof(DeletableEntity)))
        //    {
        //        return this.GetDeletableEntityRepository<T>();
        //    }

        //    return this.GetRepository<T>();
        //}

        public IRepository<User> Users
        {
            get { return GetRepository<User>(); }
        }

        /// <summary>
        ///     Saves all changes made in this context to the underlying database.
        /// </summary>
        /// <returns>
        ///     The number of objects written to the underlying database.
        /// </returns>
        /// <exception cref="T:System.InvalidOperationException">Thrown if the context has been disposed.</exception>
        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (context != null)
                {
                    context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class, IEntity
        {
            if (!repositories.ContainsKey(typeof (T)))
            {
                var type = typeof (GenericRepository<T>);
                repositories.Add(typeof (T), Activator.CreateInstance(type, context));
            }

            return (IRepository<T>) repositories[typeof (T)];
        }
    }
}