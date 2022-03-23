using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Books.Authorization;

namespace Books
{
    [DependsOn(
        typeof(BooksCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class BooksApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<BooksAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(BooksApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
