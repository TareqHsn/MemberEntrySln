using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace MemberEntry.Pages.Profile
{
    public class ManageRolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public ManageRolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public List<IdentityRole> Roles { get; set; }

        public async Task OnGetAsync()
        {
            Roles = await _roleManager.Roles.ToListAsync();
        }
    }
}

