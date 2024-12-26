using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Thanks
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        [StringLength(700)]
        public string Text { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public Category Category { get; set; }
        public User User { get; set; }
        public List<ThanksTag> ThanksTags { get; set; } = new List<ThanksTag>();
    }
}
