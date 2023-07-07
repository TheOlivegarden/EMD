using EMD.Web.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EMD.Web.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public Register RegisterViewModel { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByNameAsync(RegisterViewModel.Username);
                if (existingUser != null)
                {
                    TempData["ErrorMessage"] = "Username already exists. Please choose a different username.";
                    return Page();
                }

                var existingEmail = await _userManager.FindByEmailAsync(RegisterViewModel.Email);
                if (existingEmail != null)
                {
                    TempData["ErrorMessage"] = "Email already exists. Please choose a different email.";
                    return Page();
                }

                var user = new IdentityUser
                {
                    UserName = RegisterViewModel.Username,
                    Email = RegisterViewModel.Email
                };

                var identityResult = await _userManager.CreateAsync(user, RegisterViewModel.Password);

                if (identityResult.Succeeded)
                {
                    TempData["SuccessMessage"] = "User registered successfully.";
                    return RedirectToPage("/Login");
                }
                else
                {
                    foreach (var error in identityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return Page();
        }
    }
}