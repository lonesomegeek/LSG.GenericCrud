using System;
using System.Collections.Generic;
using System.Text;

namespace LSG.GenericCrud.Models
{
    public class EntityUserStatus : IEntity
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string EntityName { get; set; }
        public Guid EntityId { get; set; }
        public DateTime LastViewed { get; set; }
    }
}
