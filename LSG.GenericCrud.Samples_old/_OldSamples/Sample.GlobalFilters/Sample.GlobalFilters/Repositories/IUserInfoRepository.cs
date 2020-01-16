using System;

namespace Sample.GlobalFilters.Repositories
{
    public interface IUserInfoRepository
    {
        Guid TenantId { get; }
    }
}
