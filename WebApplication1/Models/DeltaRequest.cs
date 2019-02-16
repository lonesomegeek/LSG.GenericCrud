using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WebApplication1.Controllers;

namespace WebApplication1.Models
{
    public class DeltaRequest
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public DeltaRequestModes Mode { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
    }


}
