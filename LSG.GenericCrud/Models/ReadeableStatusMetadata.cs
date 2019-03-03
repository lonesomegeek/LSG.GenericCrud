using System;

namespace LSG.GenericCrud.Models
{
    public class ReadeableStatusMetadata
    {
        public bool NewStuffAvailable { get; internal set; }
        public DateTime? LastViewed { get; internal set; }
    }
}