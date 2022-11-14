using FlapKap.Core.Enums;

namespace FlapKap.API.APIRequests.User
{
    public class UserAPIRequest
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public UserRole RoleId { get; set; }
        
    }
}
