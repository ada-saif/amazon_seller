using AmazonRegistration.Model;
using AmazonSellerApi.Model;

namespace AmazonRegistration.Interface
{
    public interface IRegistrationInterface
    {
        public Response UserRegistration(RegistrationModel userModel);
        public Response UserLogin(Login login);
        public Task<Response> GenerateAccessTokenByAuth(authModel user);

        public string Salt(string Original);
        public Response ValidateOtp(validateOtp otp);
        public Response GetbyUserId(int userId);
        public Response UpdateUser(RegistrationModel model);


    }
}
