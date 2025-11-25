using CarShop.Models;

namespace CarShop.Services
{
    public class SessionService
    {
        public User? CurrentUser { get; private set; }

        public bool IsLoggedIn => CurrentUser != null;
        public bool IsAdmin => CurrentUser != null && CurrentUser.Role == 0;
        public bool IsVendor => CurrentUser != null && CurrentUser.Role == 1;

        public void SignIn(User user)
        {
            CurrentUser = user;
        }

        public void SignOut()
        {
            CurrentUser = null;
        }
    }
}
