namespace BLL.DAL
{
    public class ThanksTag
    {
        public int Id { get; set; }
        public int ThanksId { get; set; }
        public Thanks Thanks { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
