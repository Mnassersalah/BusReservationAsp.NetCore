using BusSystem.Data;
using BusSystem.Models;
using BusSystem.Services;
using BusSystem.utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusSystem
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();


            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.SignIn.RequireConfirmedAccount = false;

            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders()
              .AddDefaultUI();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddAuthentication()
                    .AddGoogle(options =>
                    {
                        options.ClientId = "781909915244-vgi61hpmr5recdbe6b8bd5jdeoihbft5.apps.googleusercontent.com";
                        options.ClientSecret = "-Gu4SxrxZ7vpy04sUCibHClz";
                    })
                    .AddFacebook(options =>
                    {
                        options.AppId = "419357189359106";
                        options.AppSecret = "5a20bc6c6bb938181e0a0d74884b03d8";
                    });

            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);


            services.AddScoped<IRepository<Bus>, BusesService>();
            services.AddScoped<IRepository<Station>, StationRepository>();
            services.AddScoped<IRepository<Ticket>, TicketService>();
            services.AddScoped<IRepository<Trip>, TripService>();
            services.AddScoped<IRepository<Route>, RoutesService>();
            services.AddScoped<IRepository<ApplicationUser>, ClientService>();
            services.AddScoped<IEmailSender, EmailSender>();
            services.Configure<StripeSettings>(Configuration.GetSection("stripe"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // stripe
            StripeConfiguration.SetApiKey(Configuration.GetSection("stripe")["SecretKey"]);

            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseExceptionHandler("/Error");
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

