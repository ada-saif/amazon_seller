namespace AmazonRegistration.Model
{
    public class Response
    {
        public bool Status { get; set; }

        public string Message { get; set; }

        public object ResponseObject { get; set; }

        public Response()
        {
        }

        public Response(bool status, string message = "", object responseObject = null)
        {
            Status = status;
            Message = message;
            ResponseObject = responseObject;
        }
    }
}
