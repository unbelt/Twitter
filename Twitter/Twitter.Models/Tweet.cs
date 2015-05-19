namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;

    using Twitter.Contracts;

    public class Tweet : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}