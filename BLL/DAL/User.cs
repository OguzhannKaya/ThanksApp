using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace BLL.DAL
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }
        [Required]
        [StringLength(20)]
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsActive { get; set; }
        public bool Gender {  get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
