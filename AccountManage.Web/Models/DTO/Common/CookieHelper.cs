namespace AccountManagement.Web.Models.DTO.Common
{
    public class CookieHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CookieHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasCookie(string cookieName)
        {
            return _httpContextAccessor.HttpContext?.Request?.Cookies?.ContainsKey(cookieName) ?? false;
        }
    }
}
