using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.Abp.VerificationCode.Identity
{
    public class AbpVerificationCodePhoneTokenProvider : AbpVerificationCodeTokenProvider
    {
        public AbpVerificationCodePhoneTokenProvider(IVerificationCodeManager verificationCodeManager)
            : base(verificationCodeManager)
        {
        }
        
        public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<IdentityUser> manager, IdentityUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }
            
            var phoneNumber = await manager.GetPhoneNumberAsync(user);
            
            return !string.IsNullOrWhiteSpace(phoneNumber) && await manager.IsPhoneNumberConfirmedAsync(user);
        }

        protected override async Task<string> GetCacheKeyAsync(string purpose, UserManager<IdentityUser> manager,
            IdentityUser user)
        {
            if (manager == null)
            {
                throw new ArgumentNullException(nameof(manager));
            }

            var phoneNumber = await manager.GetPhoneNumberAsync(user);

            return "PhoneNumber:" + purpose + ":" + phoneNumber;
        }
    }
}