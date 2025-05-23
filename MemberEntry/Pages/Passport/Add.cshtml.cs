using MemberEntry.Interfaces;
using MemberEntry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemberEntry.Pages.Passport
{
    public class AddModel : PageModel
    {
        private readonly IPassprtTypeRepository _passprtTypeRepository;

        public AddModel(IPassprtTypeRepository passprtTypeRepository)
        {
            _passprtTypeRepository = passprtTypeRepository;
        }

        [BindProperty]
        public PassportType PassportType { get; set; } = default!;

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
           
            await _passprtTypeRepository.AddAsync(PassportType);

            return RedirectToPage("./List");  // Redirect after successful add
        }
    }

}


