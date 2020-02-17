using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace SampleWebStore.Authorization
{
    public class SampleWebStoreAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.PagesUsers, L("Users"));
            context.CreatePermission(PermissionNames.PagesRoles, L("Roles"));
            context.CreatePermission(PermissionNames.PagesTenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            context.CreatePermission(PermissionNames.PagesShops, L("Shops"));
		}

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, SampleWebStoreConsts.LocalizationSourceName);
        }
    }
}
