using System;

namespace LSG.GenericCrud.Models
{
    /// <summary>
    /// The base entity.
    /// </summary>
    public abstract class BaseEntity : IEntity
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get; set; }

        public Guid Id { get; set; }
    }
}
