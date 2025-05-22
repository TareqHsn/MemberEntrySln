using MemberEntry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemberEntry.Pages.Profile
{
    //[Authorize(Roles = "Admin")]
    public class ManageUsersModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public List<UserViewModel> Users { get; set; }

        public class UserViewModel
        {
            public string Id { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public List<string> Roles { get; set; }
            public List<string> Permissions { get; set; }
        }

        public async Task OnGetAsync()
        {
            Users = new List<UserViewModel>();
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                var permissions = (await _userManager.GetClaimsAsync(user))
                    .Where(c => c.Type == "Permission")
                    .Select(c => c.Value)
                    .ToList();
                Users.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Roles = roles.ToList(),
                    Permissions = permissions
                });
            }
        }
    }
}
