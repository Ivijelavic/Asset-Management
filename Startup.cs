using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCappCoreWeb.EfDbLayer;
using MVCappCoreWeb.Models;
using MVCappCoreWeb.Permission;

namespace MVCappCoreWeb
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;
        //public Startup(IConfiguration configuration)
        //{
        //    Configuration = configuration;
        //}

        public IConfiguration Configuration { get; }
       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<OrbisDbContext>(
            options => options.UseSqlServer(Configuration.GetConnectionString("OrbisConnection")));

               // services.AddScoped<IUgovoriRepository, SQLUgovoriRepository>();
               // services.AddScoped<IUgovoriRepository, MockUgovoriRepository>();

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddControllersWithViews();
            services.AddRazorPages();

           




            services.ConfigureApplicationCookie(opts =>
            {
                opts.AccessDeniedPath = "/AccessDenied/AccessDenied";
            });
            //services.AddIdentity<WebUser, IdentityRole>()
            //        .AddEntityFrameworkStores<MVCappCoreWebContext>()
            //        .AddDefaultTokenProviders();
            //services.AddScoped<IUserClaimsPrincipalFactory<WebUser>,
            //AdditionalUserClaimsPrincipalFactory>();
            /**********************************************************************/
            //services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            //services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //services.AddDbContext<MVCappCoreWebContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddIdentity<IdentityUser, IdentityRole>()
            //        .AddEntityFrameworkStores<MVCappCoreWebContext>()
            //        .AddDefaultUI()
            //.AddDefaultTokenProviders();
            //services.AddControllersWithViews();
           

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
