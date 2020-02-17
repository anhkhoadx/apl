using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using SampleWebStore.Roles.Dto;
using SampleWebStore.Users.Dto;

namespace SampleWebStore.Users
{
    public interface IUserAppService : IAsyncCrudAppService<UserDto, long, PagedUserResultRequestDto, CreateUserDto, UserDto>
    {
        Task<ListResultDto<RoleDto>> GetRoles();

        Task ChangeLanguage(ChangeUserLanguageDto input);
    }
}
