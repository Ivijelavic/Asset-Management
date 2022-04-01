using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVCappCoreWeb.Areas.Identity.Data;
using MVCappCoreWeb.Data;
using MVCappCoreWeb.EfDbLayer;
using MVCappCoreWeb.Models;

[assembly: HostingStartup(typeof(MVCappCoreWeb.Areas.Identity.IdentityHostingStartup))]
namespace MVCappCoreWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MVCappCoreWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DefaultConnection")
                        ));

                //services.AddIdentity<WebUser, IdentityRole>()
                //                   .AddEntityFrameworkStores<MVCappCoreWebContext>()
                //                   .AddDefaultTokenProviders();

                services.AddIdentity<WebUser, IdentityRole>()
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddRoleManager<RoleManager<IdentityRole>>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<MVCappCoreWebContext>();
               
                
               //services.AddTransient<IUgovoriRepository,SQLUgovoriRepository>();
                //services.AddTransient<IUgovoriRepository, MockUgovoriRepository>()
                        
               // services.AddTransient<IUgovoriRepository, MockUgovoriRepository>()
                         //.AddRouting();

                //services.AddDefaultIdentity<WebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<MVCappCoreWebContext>();
            });
        }
    }
}
