using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using Books.Configuration.Dto;

namespace Books.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : BooksAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
