using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberEntry.Models
{
    public class MemberBasicInfoModel : AuditModel
    {
        [Key]
        public int MemberId { get; set; }

        [DisplayName("Identity No")]
        public string IdentityNo { get; set; }

        [Required(ErrorMessage = "Please enter a 'Name in English'.")]
        [MinLength(3, ErrorMessage = "Minimum length of 'Name in English' is 3 characters.")]
        [MaxLength(100, ErrorMessage = "Maximum length of 'Name in English' is 100 characters.")]
        [DisplayName("Name in English")]
        public string? NameInEnglish { get; set; }

        public int? PassportTypeId { get; set; }
        public PassportType? PassportType { get; set; }

        [Required(ErrorMessage = "Please enter a 'Name in Bangla'.")]
        [MinLength(3, ErrorMessage = "Minimum length of 'Name in Bangla' is 3 characters.")]
        [MaxLength(100, ErrorMessage = "Maximum length of 'Name in Bangla' is 100 characters.")]
        [DisplayName("Name in Bangla")]
        public string? NameInBangla { get; set; }


        [DisplayName("Father Name")]
        public string? Father { get; set; }



        [MinLength(3, ErrorMessage = "Minimum length of 'Mother Name' is 3 characters.")]
        [MaxLength(100, ErrorMessage = "Maximum length of 'Mother Name' is 100 characters.")]

        [DisplayName("Mother Name")]
        public string? Mother { get; set; }
       

        [DisplayName("Political View")]
        public string? PoliticalView { get; set; }

        [DisplayName("Is Active")]
        public bool IsActive { get; set; }

       
        [DisplayName("Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DOB { get; set; }

        public string? ImagePath { get; set; }
        public string? CreatedByName { get; set; }
        public string? Organization { get; set; }
        public string? Designation { get; set; }

        [DisplayName("Gender")]
        public string? GenderName { get; set; }
        [DisplayName("Religion")]
        public string? ReligionName { get; set; }
        [DisplayName("Nationality")]
        public string? NationalityName { get; set; }
        [DisplayName("Marital Status")]
        public string? MaritalStatusName { get; set; }
        [DisplayName("Citizen")]
        public string? CitizenName { get; set; }
        [DisplayName("Profession")]
        public string? ProfessionName { get; set; }
       
        [DisplayName("Passport Number")]
        public string? PassportNumber { get; set; }
        [DisplayName("NID Number")]
        public string? NIDNumber { get; set; }
      

        [DisplayName("Address")]
        public string? Address { get; set; }
        #region derived
        public string DOBSt
        {
            get
            {
                return this.DOB.HasValue ? this.DOB.Value.ToString("dd MMM yyyy") : "";
            }
        }
        #endregion

        #region IDnName
        public string IDnName
        {
            get
            {
                return $"{this.IdentityNo} {this.NameInEnglish}";
            }
        }
        #endregion
        
    }
}
