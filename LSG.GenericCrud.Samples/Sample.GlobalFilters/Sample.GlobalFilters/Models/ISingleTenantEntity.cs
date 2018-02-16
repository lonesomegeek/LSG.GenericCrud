using System;

namespace Sample.GlobalFilters.Models
{
    public interface ISingleTenantEntity
    {
        Guid TenantId { get; set; }
    }
}
