using MemberEntry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemberEntry.Pages.Profile
{
    //[Authorize(Roles = "Admin")]
    public class AssignPermissionsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignPermissionsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public string UserId { get; set; }

        public string UserEmail { get; set; }

        public List<string> AvailablePermissions { get; } = new List<string>
        {
            Permissions.CanEditProfile,
            Permissions.CanDeleteUsers,
            Permissions.CanViewReports
        };

        [BindProperty]
        public List<string> SelectedPermissions { get; set; } = new List<string>();

        public List<string> CurrentPermissions { get; set; } = new List<string>();

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return NotFound("User ID is required.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            UserId = userId;
            UserEmail = user.Email;
            CurrentPermissions = (await _userManager.GetClaimsAsync(user))
                .Where(c => c.Type == "Permission")
                .Select(c => c.Value)
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Remove existing permission claims
            var existingClaims = await _userManager.GetClaimsAsync(user);
            var permissionClaims = existingClaims.Where(c => c.Type == "Permission").ToList();
            foreach (var claim in permissionClaims)
            {
                await _userManager.RemoveClaimAsync(user, claim);
            }

            // Add selected permission claims
            foreach (var permission in SelectedPermissions)
            {
                await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("Permission", permission));
            }

            return RedirectToPage("/Profile/ManageUsers");
        }
    }
}
