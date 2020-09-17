using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Recruiting.Data.Data;

namespace Recruiting.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<RecruitingContext>();
                    SeedRecruitingContext.Initialize(context);

                    var identityContext = services.GetRequiredService<IdentityContext>();
                    var roleManager = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                    var userManager = scope.ServiceProvider.GetService<UserManager<RecruitingUser>>();
                    var config = host.Services.GetRequiredService<IConfiguration>();
                    string defaultPwd = config["DefaultUSerPwd"];
                    SeedIdentityContext.Initialize(identityContext, roleManager, userManager, defaultPwd).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
