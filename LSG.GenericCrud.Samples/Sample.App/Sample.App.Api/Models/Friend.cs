using System;
using LSG.GenericCrud.Models;

namespace Sample.App.Api.Models
{
    public class Friend : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PictureUrl { get; set; }
    }
}
