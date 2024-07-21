namespace Account.AuthAPI.Models.Common
{
    public static class FixedRoles
    {
        public const string Customer = "Customer";
        public const string Admin = "Admin";
        public const string SuperAdmin = "SuperAdmin";

        public static List<string> GetAllRoles()
        {
            var roles = new List<string>
            {
                Customer,
                Admin,
                SuperAdmin
            };
            return roles;
        }
    }

    public static class AuthorizePolicy
    {
        public const string AdminRole = "RequireAdminRole";
        public const string CustomerRole = "RequireCustomerRole";
        public const string SuperAdminRole = "RequireSuperAdminRole";
    }
}
