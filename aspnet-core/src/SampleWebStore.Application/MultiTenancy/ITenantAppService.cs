using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SampleWebStore.MultiTenancy.Dto;

namespace SampleWebStore.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

