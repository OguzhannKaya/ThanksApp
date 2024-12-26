using BLL.DAL;
using System.ComponentModel;

namespace BLL.Models
{
    public class ThanksModel
    {
        public Thanks Record { get; set; }
        public string Title => Record.Title;
        public string Text => Record.Text;
        [DisplayName("Created At")]
        public string CreatedAt => Record.CreatedAt.HasValue ? Record.CreatedAt.Value.ToString("MM/dd/yyyy") : string.Empty;
        public string Category => Record.Category?.Name;
        public string User => Record.User?.UserName;
        public string Tags => string.Join("<br>", Record.ThanksTags?.Select(t => t.Tag?.Name));
        [DisplayName("Tags")]
        public List<int> TagIds
        {
            get => Record.ThanksTags?.Select(tt => tt.TagId).ToList();
            set => Record.ThanksTags = value.Select(a => new ThanksTag() { TagId = a}).ToList();
        }
    }
}
