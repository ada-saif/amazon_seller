using AmazonRegistration.Model;

namespace AmazonRegistration.Interface
{
    public interface IProfile
    {
        public UserProfileResponse GetUserProfile(string AccessToken);
        public Response SaveCredentials(SellerDetail sellerDetail);
        public int? GetUserCount(int? userId);
        public Response GetUserDetail(int UserId);


    }
}
