using AmazonRegistration.Model;
using AmazonSellerApi.Model;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace AmazonSellerApi.Repo
{
    public class CommonMethods
    {
        public static string GenerateOTP()
        {
            Random random = new Random();
            int otpNumber = random.Next(10000, 99999);
            return otpNumber.ToString();
        }
       
        public static string SendEmail(string recipient)
        {
            var otp = GenerateOTP();
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Credentials = new NetworkCredential("jassyroy3@gmail.com", "ffaqkbthjdnxadun");
            var sb = new StringBuilder();
            sb.AppendLine("<html><body>");
            sb.AppendLine("<img src='cid:logo'/>");
            sb.AppendLine("<h2>Your OTP for login is:</h2>");
            sb.AppendLine($"<p style='font-size: 24px'>{otp}</p>");
            sb.AppendLine("</body></html>");
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(sb.ToString(), null, "text/html");
            string logoPath = @"C:\Users\lenovo.T460\Pictures\Camera Roll\o.png";
            if (File.Exists(logoPath))
            {
                LinkedResource logo = new LinkedResource(logoPath);
                logo.ContentId = "logo";
                htmlView.LinkedResources.Add(logo);
            }

            MailMessage message = new MailMessage();
            message.From = new MailAddress("jassyroy3@gmail.com");
            message.To.Add(recipient);
            message.Subject = "Your OTP";
            message.AlternateViews.Add(htmlView);
            client.Send(message);
            return otp;
        }


    }
}
