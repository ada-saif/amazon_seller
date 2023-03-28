using AmazonRegistration.Model;
using AmazonSellerApi.Interface;
using AmazonSellerApi.Model;

namespace AmazonSellerApi.Repo
{
    public class UserSubscriptionRepo : IUserSubsriptionRepo
    {
        private readonly ApplicationDbContext db;
        public UserSubscriptionRepo(ApplicationDbContext db)
        {
            this.db = db;
        }

        public Response Savesubs(UserSubscription subscription)
        {
            Response response=new Response();
            if (subscription == null) { response.Message = "please provide the detail";return response;}
            if (string.IsNullOrEmpty(subscription.sub_name)) { response.Message = "Please provide subscription name"; return response; }
            if (string.IsNullOrEmpty(subscription.region)) { response.Message = "Please provide subscription name"; return response; }
            var data = db.tbl_user_subsription.FirstOrDefault(a => a.user_id == subscription.user_id);
            if(data != null) { response.Message = "It is already subscribed to this user";return response;}
            UserSubscription subscription1 = new UserSubscription();
            subscription1.sub_name = subscription.sub_name;
            subscription1.region = subscription.region;
            subscription1.user_id = subscription.user_id;
            subscription1.is_active = false;
            db.tbl_user_subsription.Add(subscription1);
            int res = db.SaveChanges();
            if (res > 0)
            {
                response.Status = true;
                response.Message = "Subscription add succeessfully";
                response.ResponseObject = subscription1;
                return response;
            }
            else
            {
                response.Status = false;
                response.Message = "no data add";
                return response;
            }


        }
    }
}
