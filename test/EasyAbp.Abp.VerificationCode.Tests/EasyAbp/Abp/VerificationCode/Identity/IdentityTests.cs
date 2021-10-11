using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Shouldly;
using Volo.Abp.Identity;
using Xunit;

namespace EasyAbp.Abp.VerificationCode.Identity
{
    public class IdentityTests : VerificationCodeTestBase<AbpVerificationCodeTestsModule>
    {
        private static readonly Guid UserId = Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d");

        [Fact]
        public async Task Should_Change_Phone_Number()
        {
            const string newPhoneNumber = "123456";

            var userManager = ServiceProvider.GetRequiredService<IdentityUserManager>();
            var identityOptions = ServiceProvider.GetRequiredService<IOptions<IdentityOptions>>();

            await identityOptions.SetAsync();

            var user = await userManager.GetByIdAsync(UserId);

            var token = await userManager.GenerateChangePhoneNumberTokenAsync(user, newPhoneNumber);
            
            token.Length.ShouldBe(6);

            var identityResult = await userManager.ChangePhoneNumberAsync(user, newPhoneNumber, token);
            
            identityResult.Succeeded.ShouldBe(true);
        }
        
        [Fact]
        public async Task Should_Reset_Password()
        {
            const string newPassword = "1q2w3E*123";

            var userManager = ServiceProvider.GetRequiredService<IdentityUserManager>();
            var identityOptions = ServiceProvider.GetRequiredService<IOptions<IdentityOptions>>();

            await identityOptions.SetAsync();
            
            var user = await userManager.GetByIdAsync(UserId);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            
            token.Length.ShouldBe(6);

            var identityResult = await userManager.ResetPasswordAsync(user, token, newPassword);
            
            identityResult.Succeeded.ShouldBe(true);
        }
        
        [Fact]
        public async Task Should_Change_Email()
        {
            const string newEmail = "mynewemail@abp.io";

            var userManager = ServiceProvider.GetRequiredService<IdentityUserManager>();
            var identityOptions = ServiceProvider.GetRequiredService<IOptions<IdentityOptions>>();

            await identityOptions.SetAsync();
            
            var user = await userManager.GetByIdAsync(UserId);

            var token = await userManager.GenerateChangeEmailTokenAsync(user, newEmail);
            
            token.Length.ShouldBe(6);

            var identityResult = await userManager.ChangeEmailAsync(user, newEmail, token);
            
            identityResult.Succeeded.ShouldBe(true);
        }
        
        [Fact]
        public async Task Should_Confirm_Email()
        {
            var userManager = ServiceProvider.GetRequiredService<IdentityUserManager>();
            var identityOptions = ServiceProvider.GetRequiredService<IOptions<IdentityOptions>>();

            await identityOptions.SetAsync();
            
            var user = await userManager.GetByIdAsync(UserId);

            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            
            token.Length.ShouldBe(6);

            var identityResult = await userManager.ConfirmEmailAsync(user, token);
            
            identityResult.Succeeded.ShouldBe(true);
        }
    }
}
