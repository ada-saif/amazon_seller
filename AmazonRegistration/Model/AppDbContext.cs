using AmazonSellerApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AmazonRegistration.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<RegistrationModel> tbl_user { get; set; }
        public DbSet<Order> tbl_order { get; set; }
        public DbSet<SellerDetail> tbl_user_profile { get; set; }
        public DbSet<UserSubscription> tbl_user_subsription { get; set; }




    }
}
