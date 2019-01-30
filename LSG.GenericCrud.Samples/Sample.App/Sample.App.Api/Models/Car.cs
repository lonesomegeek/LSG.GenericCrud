using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LSG.GenericCrud.Models;

namespace Sample.App.Api.Models
{
    public class Car : IEntity
    {
        public Guid Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public string PictureUrl { get; set; }

        //public IEnumerable<Friend> Friends { get; set; }
    }
}
