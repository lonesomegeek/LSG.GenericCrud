
using System;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Samples.Models.Entities
{
    public class Hook : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string URL { get;set; }
        public string EntityId { get;set; }
        public string EntityName { get;set; }
    }
}