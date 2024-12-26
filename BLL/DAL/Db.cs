using Microsoft.EntityFrameworkCore;

namespace BLL.DAL
{
    public class Db : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Thanks> Thanks { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ThanksTag> ThanksTags { get; set; }
        
        public Db(DbContextOptions options) : base(options)
        {
                
        }
    }
}
