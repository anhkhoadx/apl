using Abp.Authorization;
using SampleWebStore.Authorization.Roles;
using SampleWebStore.Authorization.Users;

namespace SampleWebStore.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
