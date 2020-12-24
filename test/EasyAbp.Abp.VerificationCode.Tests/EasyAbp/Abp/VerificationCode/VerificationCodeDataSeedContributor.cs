using System;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.Uow;

namespace EasyAbp.Abp.VerificationCode
{
    public class VerificationCodeDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IdentityUserManager _identityUserManager;

        public VerificationCodeDataSeedContributor(
            IdentityUserManager identityUserManager)
        {
            _identityUserManager = identityUserManager;
        }
        
        [UnitOfWork]
        public async Task SeedAsync(DataSeedContext context)
        {
            /* Instead of returning the Task.CompletedTask, you can insert your test data
             * at this point!
             */

            await _identityUserManager.CreateAsync(new IdentityUser(Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d"),
                "testMan", "testMan@abp.io"));
        }
    }
}