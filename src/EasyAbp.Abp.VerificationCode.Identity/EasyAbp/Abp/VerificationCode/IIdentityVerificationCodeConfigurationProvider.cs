using System.Threading.Tasks;

namespace EasyAbp.Abp.VerificationCode
{
    public interface IIdentityVerificationCodeConfigurationProvider
    {
        Task<VerificationCodeConfiguration> GetAsync();
    }
}