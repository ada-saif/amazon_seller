using AmazonRegistration.Model;

namespace AmazonRegistration.Interface
{
    public interface IGanrateAccessToken
    {
        public Task<AccessTokenData> GetToken(int p_id);

    }
}
