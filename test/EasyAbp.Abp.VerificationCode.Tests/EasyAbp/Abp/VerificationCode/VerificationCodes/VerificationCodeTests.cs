using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace EasyAbp.Abp.VerificationCode.VerificationCodes
{
    public class VerificationCodeTests : VerificationCodeTestBase<AbpVerificationCodeTestsModule>
    {
        [Fact]
        public async Task Should_Generate_Correct_Verification_Code()
        {
            var verificationCodeGenerator = ServiceProvider.GetRequiredService<IVerificationCodeGenerator>();

            var code = await verificationCodeGenerator.CreateAsync(new VerificationCodeConfiguration(8));
            
            code.Length.ShouldBe(8);
        }
        
        [Fact]
        public async Task Should_Pass_Verification_Code_Validation()
        {
            const string codeCacheKey = "test1";
            
            var verificationCodeManager = ServiceProvider.GetRequiredService<IVerificationCodeManager>();

            var configuration = new VerificationCodeConfiguration();

            var code = await verificationCodeManager.GenerateAsync(codeCacheKey, TimeSpan.FromMinutes(3),
                configuration);
            
            code.ShouldNotBeNullOrWhiteSpace();

            var result = await verificationCodeManager.ValidateAsync(codeCacheKey, code, configuration);
            
            result.ShouldBe(true);
        }
        
        [Fact]
        public async Task EquivalentCharsMaps_Should_Work()
        {
            const string codeCacheKey = "test2";
            
            var verificationCodeManager = ServiceProvider.GetRequiredService<IVerificationCodeManager>();

            var configuration = new VerificationCodeConfiguration(6, "oO", new Dictionary<char, IEnumerable<char>>
            {
                {'o', new[] {'0'}},
                {'O', new[] {'0'}}
            });

            var code = await verificationCodeManager.GenerateAsync(codeCacheKey, TimeSpan.FromMinutes(3),
                configuration);
            
            code.Count(x => x != 'o' && x != 'O').ShouldBe(0);

            var result = await verificationCodeManager.ValidateAsync(codeCacheKey, "000000", configuration);
            
            result.ShouldBe(true);
        }
    }
}
