using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Books.EntityFrameworkCore;
using Books.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace Books.Web.Tests
{
    [DependsOn(
        typeof(BooksWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class BooksWebTestModule : AbpModule
    {
        public BooksWebTestModule(BooksEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(BooksWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(BooksWebMvcModule).Assembly);
        }
    }
}