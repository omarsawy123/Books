using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace Books.Controllers
{
    public abstract class BooksControllerBase: AbpController
    {
        protected BooksControllerBase()
        {
            LocalizationSourceName = BooksConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
