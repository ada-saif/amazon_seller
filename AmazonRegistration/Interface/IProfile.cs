using AmazonRegistration.Model;

namespace AmazonRegistration.Interface
{
    public interface IProfile
    {
        public UserProfileResponse GetUserProfile(string AccessToken,int user_id);
        public int? GetUserCount(int? userId);
        public Response GetUserDetail(int UserId);


    }
}
