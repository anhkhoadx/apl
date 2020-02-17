using System.Threading.Tasks;
using Abp.Application.Services;
using SampleWebStore.Authorization.Accounts.Dto;

namespace SampleWebStore.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
