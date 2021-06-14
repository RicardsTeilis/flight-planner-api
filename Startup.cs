using FlightPlanner.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightPlanner
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddTransient<UserService.IUserService, UserService>();
            services.AddAuthentication("BasicAuthentication").
                AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
 
        }

        public static void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            //app.UseHttpsRedirection();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}