using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MultiStageForm.Models;

namespace MultiStageForm
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
            services.AddMvc();

            // We must register our DbContext as a Service
            // https://docs.microsoft.com/en-us/ef/core/get-started/aspnetcore/existing-db#register-your-context-with-dependency-injection
            // Here the first parameter is defined as Action<DbContextOptionsBuilder> which lets us use a Lambda expression
            // https://docs.microsoft.com/en-us/dotnet/standard/delegates-lambdas
            services.AddDbContext<MultiStageFormContext>(options => options.UseNpgsql(@"Host=localhost;Database=MultiStageForm;Username=postgres;Password="));

            // It's highly interesting that the following code doesn't compile as .UseNpgsql is defined as a static extension method.
            // services.AddDbContext<MultiStageFormContext>(DbContextOptionsBuilder.UseNpgsql(@"Host=localhost;Database=MultiStageForm;Username=postgres;Password="));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
