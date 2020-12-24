using JetBrains.Annotations;

namespace EasyAbp.Abp.VerificationCode
{
    public class VerificationCodeCacheItem
    {
        [NotNull]
        public string Code { get; }

        public VerificationCodeCacheItem(
            [NotNull] string code)
        {
            Code = code;
        }
    }
}