using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace SampleWebStore.Controllers
{
    public abstract class SampleWebStoreControllerBase: AbpController
    {
        protected SampleWebStoreControllerBase()
        {
            LocalizationSourceName = SampleWebStoreConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
