using System.Threading.Tasks;
using Books.Configuration.Dto;

namespace Books.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
