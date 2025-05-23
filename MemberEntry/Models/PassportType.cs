using System.ComponentModel.DataAnnotations;

namespace MemberEntry.Models
{
    public class PassportType
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter 'Name'.")]
        [MaxLength(100, ErrorMessage = "Maximum length of 'Name' is 100 characters.")]
        public string Name { get; set; }

        public ICollection<MemberBasicInfoModel> Members { get; set; }

        //public List<MemberBasicInfoModel> Members { get; set; } = new List<MemberBasicInfoModel>();
    }
}
