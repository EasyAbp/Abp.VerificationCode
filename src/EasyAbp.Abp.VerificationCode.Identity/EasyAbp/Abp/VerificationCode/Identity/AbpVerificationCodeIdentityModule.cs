using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace EasyAbp.Abp.VerificationCode.Identity
{
    [DependsOn(
        typeof(AbpIdentityDomainModule),
        typeof(AbpVerificationCodeModule)
    )]
    public class AbpVerificationCodeIdentityModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddTransient<AbpVerificationCodeEmailTokenProvider>();
            context.Services.AddTransient<AbpVerificationCodePhoneTokenProvider>();

            Configure<IdentityOptions>(options =>
            {
                options.Tokens.ProviderMap[TokenOptions.DefaultEmailProvider] =
                    new TokenProviderDescriptor(typeof(AbpVerificationCodeEmailTokenProvider));

                options.Tokens.ProviderMap[TokenOptions.DefaultPhoneProvider] =
                    new TokenProviderDescriptor(typeof(AbpVerificationCodePhoneTokenProvider));
                
                options.Tokens.ChangeEmailTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
                options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultEmailProvider;
                
                options.Tokens.ChangePhoneNumberTokenProvider = TokenOptions.DefaultPhoneProvider;
            });
        }
    }
}
