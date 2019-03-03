using LSG.GenericCrud.Services;

namespace LSG.GenericCrud.Models
{
    public class ReadeableStatus<T>
    {
        public T Data { get; set; }
        public ReadeableStatusMetadata Metadata { get; set; }
    }
}