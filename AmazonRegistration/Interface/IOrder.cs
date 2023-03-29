using AmazonSellerApi.Model.OrderRelatedModel;

namespace AmazonRegistration.Interface
{
    public interface Iorder
    {
        public object GetOrder();

        public  Task<OrderData> LoadDataFromAmazon(inputFeild input);

    }
}
