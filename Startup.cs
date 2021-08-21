using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using beltExamActivity.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace beltExamActivity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDefaultIdentity<ApplicationIdentityDbContext, ApplicationUser, IdentityRole>(Configuration,
            // o =>
            // {
            //     o.Password.RequireDigit = false;
            //     o.Password.RequireLowercase = false;
            //     o.Password.RequireUppercase = false;
            //     o.Password.RequireNonLetterOrDigit = false;
            //     o.Password.RequiredLength = 7;
            // });
            services.AddDbContext<BeltExamContext>(options => options.UseMySql(
            Configuration["DBInfo:ConnectionString"],
            ServerVersion.FromString("8.0.23-mysql")));

            // to access session directly from view, corresponds with: @using Microsoft.AspNetCore.Http in Views/_ViewImports.cshtml
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddMvc(options => options.EnableEndpointRouting = false);
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
            }

            // css, js, and image files can now be added to wwwroot folder
            app.UseStaticFiles();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}