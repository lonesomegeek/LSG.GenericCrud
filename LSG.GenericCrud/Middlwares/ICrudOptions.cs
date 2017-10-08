namespace LSG.GenericCrud.Middlwares
{
    public interface ICrudOptions<T>
    {
        bool IsDeleteRouteEnabled { get; set; }
        bool IsGetAllRouteEnabled { get; set; }
        bool IsGetRouteEnabled { get; set; }
        bool IsPostRouteEnabled { get; set; }
        bool IsPutRouteEnabled { get; set; }
    }
}