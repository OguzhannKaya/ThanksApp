using BLL.DAL;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.Models
{
    public class UserModel
    {
        public User Record { get; set; }
        public string UserName => Record.UserName;
        public string Password => Record.Password;
        [DisplayName("Birth Date")]
        public string BirthDate => Record.BirthDate.HasValue ? Record.BirthDate.Value.ToString("MM/dd/yyyy") : string.Empty;
        [DisplayName("Is Active")]
        public string IsActive => Record.IsActive ? "Active" : "Not Active";
        [DisplayName("Is Male")]
        public string Gender => Record.Gender ? "Male" : "Female";
        public string Role => Record.Role?.Name;
    }
}
