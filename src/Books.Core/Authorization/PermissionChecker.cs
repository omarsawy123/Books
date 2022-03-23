using Abp.Authorization;
using Books.Authorization.Roles;
using Books.Authorization.Users;

namespace Books.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
