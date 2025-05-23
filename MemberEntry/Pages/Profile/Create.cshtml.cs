using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using MemberEntry.Interfaces;
using MemberEntry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MemberEntry.Pages.Profile
{
    
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IPassprtTypeRepository _passprtTypeRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _config;

        public CreateModel(IMemberRepository memberRepository, IWebHostEnvironment environment,
            IPassprtTypeRepository passprtTypeRepository, IConfiguration config)
        {
            _memberRepository = memberRepository;
            _environment = environment;
            _passprtTypeRepository = passprtTypeRepository;
            _config = config;
        }

        public SelectList? PassportTypeSelectList { get; set; }

        [BindProperty]
        public MemberBasicInfoModel Member { get; set; }

        [BindProperty]
        [Display(Name = "Upload Image")]
        public IFormFile? ImageFile { get; set; }

        public string ErrorMessage { get; set; }

        private async Task PopulatePageElements()
        {
            List<PassportType> passportTypeModels = new();
            passportTypeModels = (List<PassportType>)await _passprtTypeRepository.GetAllAsync();
            PassportTypeSelectList = new SelectList(passportTypeModels, nameof(PassportType.Id), nameof(PassportType.Name));
        }

        public async Task<IActionResult> OnGet()
        {
            await PopulatePageElements();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await PopulatePageElements();
                return Page();
            }

            // Handle image upload
            if (ImageFile != null)
            {
                if (!ValidateCheckImage(ImageFile))
                {
                    await PopulatePageElements();
                    return Page();
                }
                Member.ImagePath = await UploadFile();
            }

            Member.CreatedDate = DateTime.Now;
            Member.LastModifiedDate = DateTime.Now;
            Member.CreatedBy = User.Identity.Name ?? "System"; 
            Member.LastModifiedBy = User.Identity.Name ?? "System";

            await _memberRepository.AddAsync(Member);
            return RedirectToPage("./Index");
        }

        #region Helper Methods
        private bool ValidateCheckImage(IFormFile fileImage)
        {
            if (fileImage != null)
            {
                string[] supportedImageTypes = _config["SiteSettings:SupportedImageTypes"]?.Split(',')
                    ?? new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var extension = Path.GetExtension(fileImage.FileName).ToLowerInvariant();
                if (!supportedImageTypes.Select(x => x.ToLower()).Contains(extension))
                {
                    ErrorMessage = "Only JPG, JPEG, PNG, and GIF files are allowed.";
                    return false;
                }

                int maxImageSizeKB = _config.GetValue<int>("SiteSettings:MaxImageSize", 5) * 1024; 
                if (fileImage.Length / 1024 > maxImageSizeKB)
                {
                    ErrorMessage = "Image file size must be less than the configured maximum size.";
                    return false;
                }
            }

            return true;
        }

        private async Task<string> UploadFile()
        {
            string uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
            string filePath = Path.Combine(uploadFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await ImageFile.CopyToAsync(fileStream);
            }

            return $"/uploads/{uniqueFileName}";
        }
        #endregion
    }
  
}

