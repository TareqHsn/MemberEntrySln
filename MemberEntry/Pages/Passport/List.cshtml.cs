using MemberEntry.Interfaces;
using MemberEntry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemberEntry.Pages.Passport
{
    public class ListModel : PageModel
    {
        private readonly IPassprtTypeRepository passportTypeRepository;

        public ListModel(IPassprtTypeRepository passportTypeRepository)
        {
            this.passportTypeRepository = passportTypeRepository;
        }
        public List<PassportType> PassportTypes { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                // Load all Passport type
                PassportTypes = (List<PassportType>)await passportTypeRepository.GetAllAsync();
                if (PassportTypes == null)
                {
                    TempData["Error"] = "Failed to load type: Repository returned null.";
                    PassportTypes = new List<PassportType>();
                }
            }

            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading type: {ex.Message}";
                PassportTypes = new List<PassportType>();
            }
        }
    }
}
