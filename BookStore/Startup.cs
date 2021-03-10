using BookStore.Models;
using BookStore.Models.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore
{
   
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddScoped<IRepository<Author>, AuthorDbRepository>();
            services.AddScoped<IRepository<Book>, BookDbRepository>();
            services.AddDbContext<BookStoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
                options.UseSqlServer(configuration.GetConnectionString("MySqlConnection"));

            });
            services.AddIdentity<IdentityUser, IdentityRole>(options=> {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<BookStoreContext>();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequiredLength = 10;
            //    options.Password.RequiredUniqueChars = 3;
            //});

            services.AddAuthorization(options => {
                options.AddPolicy("DeleteRolePolicy",
                  policy => policy.RequireClaim("DeleteRole"));
            });

            services.AddAuthorization(options => {
                options.AddPolicy("CreateRolePolicy",
                  policy => policy.RequireClaim("CreateRole"));
            });

            services.AddAuthorization(options => {
                options.AddPolicy("EditeRolePolicy",
                  policy => policy.RequireClaim("EditeRole"));
            });

            //Login By Google(External Login)
            /*services.AddAuthentication()
              .AddGoogle(options =>
              {
                  options.ClientId = "";
                  options.ClientSecret = "";
               });*/

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseMvc(route=> {
                route.MapRoute("deafult", "{controller=Books}/{action=Index}/{id?}");
                });
            app.UseStaticFiles();
            app.UseAuthentication();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapGet("/", async context =>
            //    {
            //        await context.Response.WriteAsync("Hello World!");
            //    });
            //});
        }
    }
}
