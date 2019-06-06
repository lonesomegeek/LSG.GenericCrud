namespace LSG.GenericCrud.Models
{
    public class Change
    {
        public string FieldName { get; set; }
        public object FromValue { get; set; }
        public object ToValue { get; set; }
    }
}