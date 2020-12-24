using System.Threading.Tasks;

namespace EasyAbp.Abp.VerificationCode
{
    public interface IVerificationCodeGenerator
    {
        Task<string> CreateAsync(VerificationCodeConfiguration configuration);
    }
}