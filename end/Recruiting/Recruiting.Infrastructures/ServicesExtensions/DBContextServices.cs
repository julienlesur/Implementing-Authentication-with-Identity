

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Recruiting.Data.Data;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class DBContextServices
    {
        public static IServiceCollection AddDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RecruitingContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("RecruitingContext")));

            services.AddDbContext<IdentityContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("IdentityContextConnection")));

            return services;
        }
    }
}
