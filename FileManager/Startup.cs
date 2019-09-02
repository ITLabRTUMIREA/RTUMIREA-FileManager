using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FileManager.AutoMapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FileManager.Models;
using FileManager.Models.EmailSendingOptions;
using FileManager.Services.EmailConfirmationService;
using FileManager.Services.ResetPasswordService;
using Microsoft.AspNetCore.Identity;
using FileManager.Models.Database.UserDepartmentRoles;
using FileManager.Models.DbInitialize;
using FileManager.Services.DbInitializeService;
using FileManager.Models.Database.UserSystemRoles;
using FileManager.Services.GetAccountDataService;
using SmartBreadcrumbs.Extensions;
using FileManager.Services.SmartBreadcrumbService;

namespace FileManager
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
            services.AddTransient<IEmailConfirmationService, EmailConfirmationService>();

            services.Configure<EmailSendingOptions>(Configuration.GetSection(nameof(EmailSendingOptions)));
            services.Configure<DbInitializeMainUser>(Configuration.GetSection(nameof(DbInitializeMainUser)));

            services.AddTransient<IResetPasswordService, ResetPasswordService>();
            services.AddTransient<IDbInitializeService, DbInitializeService>();
            services.AddTransient<IGetAccountDataService, GetAccountDataService>();
            services.AddTransient<ISmartBreadcrumbService, SmartBreadcrumbService>();

            services.AddBreadcrumbs(GetType().Assembly, options =>
            {
                options.TagName = "nav";
                options.TagClasses = "";
                options.OlClasses = "breadcrumb";
                options.LiClasses = "breadcrumb-item";
                options.ActiveLiClasses = "breadcrumb-item active";
                options.SeparatorElement = "<span class=\"oi ml-2 mr-2 oi-chevron-right\"></span>";
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddIdentity<User, SystemRole>(config => {
                    config.SignIn.RequireConfirmedEmail = true;
                    config.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<FileManagerContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddDbContext<FileManagerContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("FileManagerContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
