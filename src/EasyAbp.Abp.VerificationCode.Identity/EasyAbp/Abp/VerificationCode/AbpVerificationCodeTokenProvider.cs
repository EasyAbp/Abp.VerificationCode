using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace EasyAbp.Abp.VerificationCode
{
    public abstract class AbpVerificationCodeTokenProvider : IUserTwoFactorTokenProvider<IdentityUser>
    {
        private readonly IIdentityVerificationCodeConfigurationProvider _configurationProvider;
        private readonly IVerificationCodeManager _verificationCodeManager;

        public AbpVerificationCodeTokenProvider(
            IIdentityVerificationCodeConfigurationProvider configurationProvider,
            IVerificationCodeManager verificationCodeManager)
        {
            _configurationProvider = configurationProvider;
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
        
        protected virtual async Task<VerificationCodeConfiguration> GetVerificationCodeConfigurationAsync()
        {
            return await _configurationProvider.GetAsync();
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