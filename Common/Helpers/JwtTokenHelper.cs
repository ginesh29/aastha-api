using Microsoft.AspNetCore.Http;
using System.Linq;

namespace AASTHA2.Common.Helpers
{
    public class JwtTokenHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public JwtTokenHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string ExtractToken(string key)
        {
            var User = _httpContextAccessor.HttpContext.User;
            var value = User.Claims.FirstOrDefault(claim => claim.Type == key).Value;
            return value;
        }
    }
}
