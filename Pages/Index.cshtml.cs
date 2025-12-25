using GymTrackerProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymTrackerProject.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public bool IsSignedIn { get; private set; }
        public string? UserName { get; private set; }
        public IndexModel(SignInManager<ApplicationUser> manager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = manager;
            _userManager = userManager; 
        }

        public void OnGet()
        {
            IsSignedIn = _signInManager.IsSignedIn(User);
            if (IsSignedIn)
            {
                var UserName = _userManager.GetUserName(User);
            }
        }
    }
}
