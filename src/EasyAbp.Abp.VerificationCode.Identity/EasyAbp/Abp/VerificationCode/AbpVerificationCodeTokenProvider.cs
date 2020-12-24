using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Identity;

namespace EasyAbp.Abp.VerificationCode.Identity
{
    public abstract class AbpVerificationCodeTokenProvider : IUserTwoFactorTokenProvider<IdentityUser>
    {
        private readonly IVerificationCodeManager _verificationCodeManager;

        public AbpVerificationCodeTokenProvider(
            IVerificationCodeManager verificationCodeManager)
        {
            _verificationCodeManager = verificationCodeManager;
        }
        
        public virtual async Task<string> GenerateAsync(string purpose, UserManager<IdentityUser> manager, IdentityUser user)
        {
            return await _verificationCodeManager.GenerateAsync(
                await GetCacheKeyAsync(purpose, manager, user),
                await GetLifespanAsync(),
                await GetVerificationCodeConfigurationAsync()
            );
        }

        public virtual async Task<bool> ValidateAsync(string purpose, string token, UserManager<IdentityUser> manager, IdentityUser user)
        {
            return await _verificationCodeManager.ValidateAsync(
                await GetCacheKeyAsync(purpose, manager, user),
                token,
                await GetVerificationCodeConfigurationAsync()
            );
        }
        
        protected virtual Task<VerificationCodeConfiguration> GetVerificationCodeConfigurationAsync()
        {
            return Task.FromResult(new VerificationCodeConfiguration());
        }
        
        protected virtual Task<TimeSpan> GetLifespanAsync()
        {
            return Task.FromResult(TimeSpan.FromMinutes(3));
        }

        public abstract Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<IdentityUser> manager, IdentityUser user);

        protected abstract Task<string> GetCacheKeyAsync(string purpose, UserManager<IdentityUser> manager,
            IdentityUser user);
    }
}