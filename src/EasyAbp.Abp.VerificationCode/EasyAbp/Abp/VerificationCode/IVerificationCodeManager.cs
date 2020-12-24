using System;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace EasyAbp.Abp.VerificationCode
{
    public interface IVerificationCodeManager
    {
        Task<string> GenerateAsync(string codeCacheKey, TimeSpan codeCacheLifespan, [NotNull] VerificationCodeConfiguration configuration);

        Task<bool> ValidateAsync(string codeCacheKey, string verificationCode, [NotNull] VerificationCodeConfiguration configuration);
    }
}