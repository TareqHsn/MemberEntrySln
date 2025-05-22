using MemberEntry.Interfaces;
using MemberEntry.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemberEntry.Pages.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;

        public DetailsModel(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public MemberBasicInfoModel Member { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Member = await _memberRepository.GetByIdAsync(id);
            if (Member == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
