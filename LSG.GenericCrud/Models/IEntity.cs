using System;

namespace LSG.GenericCrud.Models
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEntity : IEntity<Guid>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Id { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IEntity<T>
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        T Id { get; set; }
    }
}
