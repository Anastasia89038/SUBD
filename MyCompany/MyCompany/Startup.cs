using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MyCompany.Domain;
using MyCompany.Domain.Entities;
using MyCompany.Domain.Repositories.Abstract;
using MyCompany.Domain.Repositories.MongoDb;
using MyCompany.Service;
using System;

namespace MyCompany
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            Configuration.Bind("Project", new Config());
            var mongoDbSettings = Configuration.GetSection(nameof(ServicesCompanyDatabaseSettings)).Get<ServicesCompanyDatabaseSettings>();


            services.Configure<ServicesCompanyDatabaseSettings>(
                Configuration.GetSection(nameof(ServicesCompanyDatabaseSettings)));

            services.AddSingleton<IServicesCompanyDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<ServicesCompanyDatabaseSettings>>().Value);

            services.AddTransient<IGenericRepository<TextField>, MongoTextFieldsRepository>();
            services.AddTransient<IGenericRepository<ServiceItem>, MongoServiceItemsRepository>();
            services.AddTransient<IGenericRepository<NewsItem>, MongoNewsItemsRepository>();
            services.AddTransient<DataManager>();


            services.AddControllersWithViews()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddSessionStateTempDataProvider();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) 
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
