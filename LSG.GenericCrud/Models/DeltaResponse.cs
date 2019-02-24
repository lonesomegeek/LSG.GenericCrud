using System;
using LSG.GenericCrud.Controllers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace LSG.GenericCrud.Models
{
    public class DeltaResponse<T1, T2> where T2 : class, IEntity<T1>, new()
    {
        
    }
}