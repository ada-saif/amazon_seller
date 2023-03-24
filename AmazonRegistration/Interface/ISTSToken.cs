using AmazonRegistration.Model;

namespace AmazonRegistration.Interface
{
    public interface ISTSToken
    {
        public Task<STSTokenData> GetToken(int p_id);

    }
}
