using System.Threading.Tasks;
using Abp.Application.Services;
using SampleWebStore.Sessions.Dto;

namespace SampleWebStore.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
