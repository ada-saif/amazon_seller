using Microsoft.EntityFrameworkCore;

namespace AmazonRegistration.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<RegistrationModel> tbl_user_registration { get; set; }
        public DbSet<Order> tbl_order { get; set; }
        public DbSet<SellerDetail> tbl_user_profile { get; set; }
        



    }
}
