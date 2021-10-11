using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity.AspNetCore;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.VerificationCode.Identity
{
    [DependsOn(
        typeof(AbpIdentityAspNetCoreModule),
        typeof(AbpVerificationCodeModule)
    )]
    public class AbpVerificationCodeIdentityModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IdentityBuilder>(builder =>
            {
                builder.AddTokenProvider<AbpVerificationCodeEmailTokenProvider>(TokenOptions.DefaultEmailProvider);
                builder.AddTokenProvider<AbpVerificationCodePhoneTokenProvider>(TokenOptions.DefaultPhoneProvider);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<IdentityOptions>(options =>
            {
                options.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                
                options.Tokens.ChangePhoneNumberTokenProvider = TokenOptions.DefaultPhoneProvider;
            });
        }
    }
}
