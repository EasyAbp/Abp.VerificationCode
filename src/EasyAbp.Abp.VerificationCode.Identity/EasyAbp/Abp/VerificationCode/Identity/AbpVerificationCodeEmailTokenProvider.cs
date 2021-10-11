using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.Abp.VerificationCode.Identity
{
    public class AbpVerificationCodeEmailTokenProvider : AbpVerificationCodeTokenProvider
    {
        public AbpVerificationCodeEmailTokenProvider(
            IIdentityVerificationCodeConfigurationProvider configurationProvider,
            IVerificationCodeManager verificationCodeManager)
            : base(configurationProvider, verificationCodeManager)
        {
        }
        
        public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<IdentityUser> manager, IdentityUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            
            var email = await manager.GetEmailAsync(user);
            
            return !string.IsNullOrWhiteSpace(email) && await manager.IsEmailConfirmedAsync(user);
        }

        protected override async Task<string> GetCacheKeyAsync(string purpose, UserManager<IdentityUser> manager,
            IdentityUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            var email = await manager.GetEmailAsync(user);

            return "Email:" + purpose + ":" + email;
        }
    }
}