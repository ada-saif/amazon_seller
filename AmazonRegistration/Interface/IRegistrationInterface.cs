using AmazonRegistration.Model;

namespace AmazonRegistration.Interface
{
    public interface IRegistrationInterface
    {
        public Response UserRegistration(RegistrationModel userModel);
        public Response UserLogin(Login login);
        public Task<Response> GenerateAccessTokenss(authModel user);

        public string Salt(string Original);


    }
}
