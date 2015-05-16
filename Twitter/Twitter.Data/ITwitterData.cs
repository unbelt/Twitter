namespace Twitter.Data
{
    using Twitter.Contracts;
    using Twitter.Models;

    public interface ITwitterData
    {
        ITwitterDbContext Context { get; }

        IRepository<User> Users { get; }

        int SaveChanges();
    }
}