namespace LSG.GenericCrud.Services
{
    public class HistoricalCrudServiceOptions
    {
        public static HistoricalCrudServiceOptions DefaultValues { get { return new HistoricalCrudServiceOptions { ShowMyNewStuff = false }; } }
        public bool ShowMyNewStuff { get; set; }
    }
}
