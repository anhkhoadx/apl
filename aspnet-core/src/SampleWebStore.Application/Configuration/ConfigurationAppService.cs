using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using SampleWebStore.Configuration.Dto;

namespace SampleWebStore.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : SampleWebStoreAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
