using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.Abp.VerificationCode
{
    [Dependency(TryRegister = true)]
    public class DefaultIdentityVerificationCodeConfigurationProvider : IIdentityVerificationCodeConfigurationProvider, ITransientDependency
    {
        public virtual Task<VerificationCodeConfiguration> GetAsync()
        {
            return Task.FromResult(new VerificationCodeConfiguration());
        }
    }
}