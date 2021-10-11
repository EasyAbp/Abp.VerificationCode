using System.Threading.Tasks;

namespace EasyAbp.Abp.VerificationCode.Identity
{
    public interface IIdentityVerificationCodeConfigurationProvider
    {
        Task<VerificationCodeConfiguration> GetAsync();
    }
}