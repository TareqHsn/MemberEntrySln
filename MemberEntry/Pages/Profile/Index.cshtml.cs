using MemberEntry.Interfaces;
using MemberEntry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace MemberEntry.Pages.Profile
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public IndexModel(IMemberRepository memberRepository, IWebHostEnvironment webHostEnvironment)
        {
            _memberRepository = memberRepository ?? throw new ArgumentNullException(nameof(memberRepository));
            _webHostEnvironment = webHostEnvironment;
        }

        public List<MemberBasicInfoModel> Members { get; set; }

        [BindProperty]
        public DateTime? FromDate { get; set; }

        [BindProperty]
        public DateTime? ToDate { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                // Load all members
                Members = (List<MemberBasicInfoModel>)await _memberRepository.GetAllAsync();
                if (Members == null)
                {
                    TempData["Error"] = "Failed to load members: Repository returned null.";
                    Members = new List<MemberBasicInfoModel>(); 
                }
            }
           
            catch (Exception ex)
            {
                TempData["Error"] = $"Error loading members: {ex.Message}";
                Members = new List<MemberBasicInfoModel>();
            }
        }

        public async Task<IActionResult> OnPostGeneratePdfAsync()
        {
            // Validate date inputs
            if (!FromDate.HasValue || !ToDate.HasValue)
            {
                TempData["Error"] = "Please select both From and To dates.";
                return RedirectToPage();
            }

            if (FromDate > ToDate)
            {
                TempData["Error"] = "To date must be on or after From date.";
                return RedirectToPage();
            }

            var from = FromDate.Value.Date;
            var to = ToDate.Value.Date.AddDays(1).AddTicks(-1);

            // Retrieve and filter members
            List<MemberBasicInfoModel> filteredMembers;
            try
            {
                var members = await _memberRepository.GetAllAsync();
                if (members == null)
                {
                    TempData["Error"] = "Failed to load members: Repository returned null.";
                    return RedirectToPage();
                }

                filteredMembers = members
                    .Where(m => m.CreatedDate.HasValue &&
                                m.CreatedDate.Value >= from &&
                                m.CreatedDate.Value <= to)
                    .ToList();
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Error retrieving members for report: {ex.Message}";
                return RedirectToPage();
            }

            if (filteredMembers.Count == 0)
            {
                TempData["Error"] = "No records found within the selected date range.";
                return RedirectToPage();
            }

            // Generate PDF using QuestPDF
            try
            {
                bool useLandscape = true;
                var document = Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Landscape());

                        page.Margin(1, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Arial));

                        // Header
                        page.Header()
                            .Text("Member Report")
                            .FontSize(16)
                            .Bold()
                            .FontFamily(Fonts.Arial);

                        page.Content()
                            .PaddingVertical(10)
                            .Column(column =>
                            {
                                // Date range
                                column.Item()
                                    .Text($"From: {from:yyyyMMdd} To: {to:yyyyMMdd}")
                                    .FontSize(10);

                                column.Item().PaddingVertical(5);

                                // Table
                                column.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(20);
                                        columns.RelativeColumn(20);
                                        columns.RelativeColumn(20);
                                        columns.RelativeColumn(20);
                                        columns.RelativeColumn(20);
                                        columns.RelativeColumn(20);
                                        columns.RelativeColumn(20);
                                        columns.RelativeColumn(20); 
                                        columns.RelativeColumn(20); 
                                        columns.RelativeColumn(20); 
                                        columns.RelativeColumn(20); 
                                        columns.RelativeColumn(20); 
                                    });

                                    // Table headers
                                    table.Header(header =>
                                    {
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Image").Bold();

                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Identity No").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Name").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("DOB").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Father Name").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Mother Name").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Citizen").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Political View").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Nationality").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("NID").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Designation").Bold();
                                        header.Cell().Background(Colors.Grey.Lighten3).Padding(2).Text("Status").Bold();
                                    });

                                    // Table data
                                    foreach (var m in filteredMembers)
                                    {
                                        //  filename ("ViewAttachment.jpg")
                                        string imageFileName = m.ImagePath;
                                        if (!string.IsNullOrEmpty(imageFileName))
                                        {
                                            // Remove any path segments if present, keep only the filename
                                            imageFileName = Path.GetFileName(imageFileName);
                                        }

                                        // Construct the full path to the image in wwwroot/Uploads
                                        string imageFullPath = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads", imageFileName ?? "");

                                        // Debugging: Log the path and existence
                                        System.Diagnostics.Debug.WriteLine($"ImagePath: {m.ImagePath}, Resolved Path: {imageFullPath}, Exists: {System.IO.File.Exists(imageFullPath)}");

                                        table.Cell().Border(0.5f).Padding(2).Height(50).Width(50).Element(cell =>
                                        {
                                            if (!string.IsNullOrEmpty(imageFileName) && System.IO.File.Exists(imageFullPath))
                                            {
                                                try
                                                {
                                                    var imageData = System.IO.File.ReadAllBytes(imageFullPath);
                                                    if (imageData != null && imageData.Length > 0)
                                                    {
                                                        cell.Image(imageData, ImageScaling.FitArea);
                                                    }
                                                    else
                                                    {
                                                        cell.Text("No Image Data");
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    // Log any file reading errors
                                                    System.Diagnostics.Debug.WriteLine($"Error reading image {imageFullPath}: {ex.Message}");
                                                    cell.Text("Image Error");
                                                }
                                            }
                                            else
                                            {
                                                cell.Text("No Image");
                                            }
                                        });
                                        table.Cell().Border(0.5f).Padding(2).Text(m.IdentityNo ?? "");
                                        table.Cell().Border(0.5f).Padding(2).Text(m.NameInEnglish ?? "");
                                        table.Cell().Border(0.5f).Padding(2).Text(m.DOBSt);
                                        table.Cell().Border(0.5f).Padding(2).Text(m.Father ?? "");
                                        table.Cell().Border(0.5f).Padding(2).Text(m.Mother ?? "");
                                        table.Cell().Border(0.5f).Padding(2).Text(m.CitizenName ?? "");
                                        table.Cell().Border(0.5f).Padding(2).Text(m.PoliticalView ?? "");
                                        table.Cell().Border(0.5f).Padding(2).Text(m.NationalityName ?? "");
                                        table.Cell().Border(0.5f).Padding(2).Text(m.NIDNumber);
                                        table.Cell().Border(0.5f).Padding(2).Text(m.Designation);
                                        table.Cell().Border(0.5f).Padding(2).Text(m.IsActive ? "Active" : "InActive");
                                    }
                                });
                            });
                    });
                });

                var pdfBytes = document.GeneratePdf();
                return File(pdfBytes, "application/pdf", $"MemberReport_{DateTime.Now:yyyyMMdd}.pdf");
            }

            catch (Exception ex)
            {
                TempData["Error"] = $"Error generating PDF: {ex.Message}";
                return RedirectToPage();
            }
        }
    }
}