using System.Text.Json.Serialization;

namespace LSG.GenericCrud.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum DeltaRequestModes
    {        
        Snapshot,        
        Differential
    }
}