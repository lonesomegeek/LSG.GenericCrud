
namespace LSG.GenericCrud.Extensions.Handlers
{
    public class CrudAuthorizationOptionsBuilder
    {
        private readonly CrudAuthorizationOptions _options;

        public CrudAuthorizationOptionsBuilder()
        {
            _options = new CrudAuthorizationOptions();
        }

        public CrudAuthorizationOptionsBuilder IsCreateAvailable(bool value)
        {
            _options.IsCreateAvailable = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsReadAvailable(bool value)
        {
            _options.IsReadAvailable = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsUpdateAvailable(bool value)
        {
            _options.IsUpdateAvailable = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsDeleteAvailable(bool value)
        {
            _options.IsDeleteAvailable = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsCreateProtected(bool value)
        {
            _options.IsCreateProtected = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsCreateProtected(bool value, string subscope)
        {
            _options.IsCreateProtected = value;
            _options.RequiredCreateSubscope = subscope;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsReadProtected(bool value, string subscope)
        {
            _options.IsReadProtected = value;
            _options.RequiredReadSubscope = subscope;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsUpdateProtected(bool value, string subscope)
        {
            _options.IsUpdateProtected = value;
            _options.RequiredUpdateSubscope = subscope;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsDeleteProtected(bool value, string subscope)
        {
            _options.IsDeleteProtected = value;
            _options.RequiredDeleteSubscope = subscope;
            return this;
        }

        public CrudAuthorizationOptionsBuilder IsReadProtected(bool value)
        {
            _options.IsReadProtected = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsUpdateProtected(bool value)
        {
            _options.IsUpdateProtected = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder IsDeleteProtected(bool value)
        {
            _options.IsDeleteProtected = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder CreateSubscope(string value)
        {
            _options.RequiredCreateSubscope = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder ReadSubscope(string value)
        {
            _options.RequiredReadSubscope = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder UpdateSubscope(string value)
        {
            _options.RequiredUpdateSubscope = value;
            return this;
        }
        public CrudAuthorizationOptionsBuilder DeleteSubscope(string value)
        {
            _options.RequiredDeleteSubscope = value;
            return this;
        }

        public CrudAuthorizationOptions Build()
        {
            return _options;
        }
    }
}
