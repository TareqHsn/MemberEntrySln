using MemberEntry.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MemberEntry.Models;
using MemberEntry.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MemberEntry.Pages.Profile
{
    public class EditModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IPassprtTypeRepository _passprtTypeRepository;

        public EditModel(IMemberRepository memberRepository, IWebHostEnvironment environment, IPassprtTypeRepository passprtTypeRepository)
        {
            _memberRepository = memberRepository;
            _environment = environment;
            _passprtTypeRepository = passprtTypeRepository;
           
        }

        [BindProperty]
        public MemberBasicInfoModel Member { get; set; }
        public SelectList PassportTypeSelectList { get; set; }

        [BindProperty]
        [Display(Name = "Upload New Image")]
        public IFormFile? ImageFile { get; set; }
        private async Task PopulatePageElements()
        {
            List<PassportType> passportTypeModels = new();

            passportTypeModels = (List<PassportType>)await _passprtTypeRepository.GetAllAsync();

            PassportTypeSelectList = new SelectList(passportTypeModels, nameof(PassportType.Id), nameof(PassportType.Name));

        }
        public async Task<IActionResult> OnGetAsync(int id)
        {
            Member = await _memberRepository.GetByIdAsync(id);
            await PopulatePageElements();
            if (Member == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ImageFile", "Only JPG, JPEG, PNG, and GIF files are allowed.");
                    return Page();
                }

                if (ImageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "Image file size must be less than 5MB.");
                    return Page();
                }

                if (!string.IsNullOrEmpty(Member.ImagePath))
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, Member.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var uniqueFileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                Member.ImagePath = "/uploads/" + uniqueFileName;
            }

            Member.LastModifiedDate = DateTime.Now;



            await _memberRepository.UpdateAsync(Member);
            return RedirectToPage("./Index");
        }
    }

}
