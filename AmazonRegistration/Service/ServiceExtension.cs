using AmazonRegistration.Interface;
using AmazonRegistration.Repo;
using AmazonRegistration.Repo.AmazonRegistration.Repo;

namespace AmazonRegistration.Service
{
    public static class ServicesExtension
    {
        public static IServiceCollection BindingAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IRegistrationInterface, RegistrationRepo>();
            services.AddTransient<Iorder, OrderRepo>(); 
            services.AddTransient<IProduct, ProductRepo>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IProfile, ProfileRepo>();







            return services;
        }
    }
}
