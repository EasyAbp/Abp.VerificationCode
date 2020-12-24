using Microsoft.Extensions.DependencyInjection.Extensions;
using Volo.Abp.Caching;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.VerificationCode
{
    [DependsOn(typeof(AbpCachingModule))]
    public class AbpVerificationCodeModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.TryAddTransient<IVerificationCodeGenerator, VerificationCodeGenerator>();
            context.Services.TryAddTransient<IVerificationCodeManager, VerificationCodeManager>();
        }
    }
}
