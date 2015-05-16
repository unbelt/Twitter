namespace Twitter.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Twitter.Contracts;

    public abstract class DeletableEntity : AuditInfo, IDeletableEntity
    {
        [Editable(false)]
        public bool IsDeleted { get; set; }

        [Editable(false)]
        public DateTime? DeletedOn { get; set; }
    }
}