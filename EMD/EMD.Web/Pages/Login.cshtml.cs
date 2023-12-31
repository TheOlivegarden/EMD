using EMD.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMD.Web.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }

        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(LoginViewModel.Username, LoginViewModel.Password, false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Login successful.";
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                }
            }

            return Page();
        }
    }
}