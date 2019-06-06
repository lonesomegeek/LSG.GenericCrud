using System;
using LSG.GenericCrud.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LSG.GenericCrud.Models
{
    public class DeltaRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DeltaRequestModes Mode { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }
}