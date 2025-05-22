using MemberEntry.Interfaces;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MemberEntry.Models;

namespace MemberEntry.Pages.Profile
{
    public class EditModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IWebHostEnvironment _environment;

        public EditModel(IMemberRepository memberRepository, IWebHostEnvironment environment)
        {
            _memberRepository = memberRepository;
            _environment = environment;
        }

        [BindProperty]
        public MemberBasicInfoModel Member { get; set; }

        [BindProperty]
        [Display(Name = "Upload New Image")]
        public IFormFile ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Member = await _memberRepository.GetByIdAsync(id);
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

            // Handle image upload
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Validate file type and size
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(ImageFile.FileName).ToLowerInvariant();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError("ImageFile", "Only JPG, JPEG, PNG, and GIF files are allowed.");
                    return Page();
                }

                // Limit file size to 5MB (adjust as needed)
                if (ImageFile.Length > 5 * 1024 * 1024)
                {
                    ModelState.AddModelError("ImageFile", "Image file size must be less than 5MB.");
                    return Page();
                }

                // Delete old image if it exists
                if (!string.IsNullOrEmpty(Member.ImagePath))
                {
                    var oldImagePath = Path.Combine(_environment.WebRootPath, Member.ImagePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Define uploads folder
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Save new image
                var uniqueFileName = Guid.NewGuid().ToString() + extension;
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                Member.ImagePath = "/Uploads/" + uniqueFileName;
            }

            // Update audit fields (optional)
            Member.LastModifiedDate = DateTime.Now;
            // Member.LastModifiedBy = User.Identity.Name ?? "System"; // Uncomment if authentication is enabled

            await _memberRepository.UpdateAsync(Member);
            return RedirectToPage("./Index");
        }
    }

}
