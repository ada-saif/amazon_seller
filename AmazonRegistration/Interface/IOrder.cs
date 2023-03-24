using AmazonRegistration.Model;

namespace AmazonRegistration.Interface
{
    public interface Iorder
    {
        public Object GetOrder();

        public Response LoadDataFromAmazon(inputFeild input);

    }
}
