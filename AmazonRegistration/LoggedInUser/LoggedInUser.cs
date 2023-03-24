namespace AmazonRegistration.LoggedInUser
{
    public class LoggedIn
    {
        public int user_id { get; set; }
    }

    public static class log
    { 
        public static LoggedIn GetLoggedInUser(IHttpContextAccessor contextAccessor)
        {
            if (contextAccessor != null)
            {
                var claims = contextAccessor.HttpContext.User.Claims.ToList();
                var userId = (claims.FirstOrDefault(x => x.Type == "id") != null) ? claims.FirstOrDefault(x => x.Type == "id").Value : "0";
                var loggedInUser = new LoggedIn { user_id = Convert.ToInt32(userId), };
                return loggedInUser;
            }
            else
            {
                return null;
            }

        }
    }
}
