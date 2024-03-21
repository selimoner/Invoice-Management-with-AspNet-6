using testProject.Models;

namespace testProject.ViewModels.AuthenticationViewModels
{
    public class LoginViewModel
    {
        public User User { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
