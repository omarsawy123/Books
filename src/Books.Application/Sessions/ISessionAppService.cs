using System.Threading.Tasks;
using Abp.Application.Services;
using Books.Sessions.Dto;

namespace Books.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
