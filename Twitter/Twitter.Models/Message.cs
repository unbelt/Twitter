namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;

    using Twitter.Contracts;

    public class Message : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Content { get; set; }
    }
}