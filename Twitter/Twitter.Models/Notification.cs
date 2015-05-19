namespace Twitter.Models
{
    using System.ComponentModel.DataAnnotations;

    using Twitter.Contracts;

    public class Notification : IEntity
    {
        [Key]
        public int Id { get; set; }
    }
}