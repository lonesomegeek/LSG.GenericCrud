
using System;
using LSG.GenericCrud.Models;

namespace LSG.GenericCrud.Samples.Models.Entities
{
    public class BlogPost : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Text { get;set; }
    }
}