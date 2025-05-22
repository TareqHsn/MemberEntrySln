using System.ComponentModel.DataAnnotations;
using MemberEntry.Interfaces;
using MemberEntry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MemberEntry.Pages.Profile
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(IMemberRepository memberRepository, IWebHostEnvironment environment)
        {
            _memberRepository = memberRepository;
            _environment = environment;
        }

        [BindProperty]
        public MemberBasicInfoModel Member { get; set; }

        [BindProperty]
        [Display(Name = "Upload Image")]
        public IFormFile ImageFile { get; set; }

        public IActionResult OnGet()
        {
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
                // Validate file type and size (optional)
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

                // Generate unique file name and save to wwwroot/uploads
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                //var uniqueFileName = Guid.NewGuid().ToString() + extension;
                //var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //using (var stream = new FileStream(filePath, FileMode.Create))
                //{
                //    await ImageFile.CopyToAsync(stream);
                //}

                //Member.ImagePath = "/Uploads/" + uniqueFileName;

                var folder =Path.Combine(_environment.WebRootPath, "uploads");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                var actualname= Path.GetFileName(ImageFile.FileName);
                var filePath= Path.Combine(folder, actualname);
                using(var stream = new FileStream(filePath,FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
                Member.ImagePath = "/uploads/" + actualname;



            }

           
            Member.CreatedDate = DateTime.Now;
            Member.LastModifiedDate = DateTime.Now;
            // Member.CreatedBy = User.Identity.Name ?? "System"; // Uncomment if authentication is enabled
            // Member.LastModifiedBy = User.Identity.Name ?? "System";

            await _memberRepository.AddAsync(Member);
            return RedirectToPage("./Index");
        }
    }
}

