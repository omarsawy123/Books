using System.Threading.Tasks;
using Abp.Application.Services;
using Books.Authorization.Accounts.Dto;

namespace Books.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
