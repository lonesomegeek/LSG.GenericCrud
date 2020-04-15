using LSG.GenericCrud.Helpers;
using LSG.GenericCrud.Models;
using System;

namespace LSG.GenericCrud.Samples.Models.Entities
{
    public class Share : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public Guid ContactId { get; set; }
        public Guid ObjectId { get; set; }
        public string Description { get;set; }
        public DateTime SharingReminder { get;set; }
    }
}
