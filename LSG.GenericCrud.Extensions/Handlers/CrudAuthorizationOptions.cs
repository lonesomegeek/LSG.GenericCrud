
namespace LSG.GenericCrud.Extensions.Handlers
{
    public class CrudAuthorizationOptions
    {
        public CrudAuthorizationOptions()
        {
            IsCreateProtected = false;
            IsReadProtected = false;
            IsUpdateProtected = false;
            IsDeleteProtected = false;

            IsCreateAvailable = true;
            IsReadAvailable = true;
            IsUpdateAvailable = true;
            IsDeleteAvailable = true;

            RequiredCreateSubscope = "create";
            RequiredReadSubscope = "read";
            RequiredUpdateSubscope = "update";
            RequiredDeleteSubscope = "delete";
        }
        public bool IsReadProtected { get; set; }
        public bool IsCreateProtected { get; set; }
        public bool IsUpdateProtected { get; set; }
        public bool IsDeleteProtected { get; set; }

        public string RequiredCreateSubscope { get; set; }
        public string RequiredReadSubscope { get; set; }
        public string RequiredUpdateSubscope { get; set; }
        public string RequiredDeleteSubscope { get; set; }

        public bool IsCreateAvailable { get; set; }
        public bool IsReadAvailable { get; set; }
        public bool IsUpdateAvailable { get; set; }
        public bool IsDeleteAvailable { get; set; }
    }
}
