using System.Threading.Tasks;
using SampleWebStore.Configuration.Dto;

namespace SampleWebStore.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
