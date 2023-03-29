using AmazonSellerApi.Model;
using AmazonSellerApi.Model.OrderRelatedModel;
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
        public DbSet<Profile> tbl_user_profile { get; set; }
        public DbSet<UserSubscription> tbl_user_subsription { get; set; }
        public DbSet<AccessTokenModel> tbl_access_token { get; set; }
        public DbSet<STSTokenData> tbl_sts_token { get; set; }







    }
}
