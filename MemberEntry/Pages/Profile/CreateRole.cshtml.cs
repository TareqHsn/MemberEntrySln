using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace MemberEntry.Pages.Profile
{
    public class CreateRoleModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public CreateRoleModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [StringLength(256, ErrorMessage = "The {0} must be between 2 and 256 characters long.", MinimumLength = 2)]
            public string RoleName { get; set; }
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole(Input.RoleName);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToPage("/Profile/ManageRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }
    }
}
