using Api.Core;
using Api.Core.BackgroungJobs;
using Application;
using Application.Email;
using AutoMapper;
using DataAccess;
using Domain.Etities;
using Hangfire;
using Implementation.Email;
using Implementation.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api
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

            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
            });
            services.AddControllers();
            services.AddDbContext<Context>();
            services.AddUsesCases();
            services.AddHungfire();
            services.AddJwt(appSettings);
            services.AddApplicationUser();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(typeof(GetCreditQuery).Assembly);
            services.AddTransient<IEmailSender>(x => new SmtpEmailSender(appSettings.EmailFrom, appSettings.EmailPassword));
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            /*IBackgroundJobClient backgroundJobClient,*/
            IRecurringJobManager recurringJobManager,
            IServiceProvider serviceProvider)
        {
            app.UseCors(opt => 
            opt.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader());
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }
            app.UseMiddleware<ExceptionHandler>();
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard();
            recurringJobManager.AddOrUpdate(
                "Monthly maintenance charge",
                () => serviceProvider.GetService<MonthlyMaintenanceJob>().Charge(),
                "0 12 1 * *"
                );
            recurringJobManager.AddOrUpdate(
                "Monthly paymaent charge",
                () => serviceProvider.GetService<MonthlyPaymentJob>().Charge(),
                "0 12 1 * *"
                );
            recurringJobManager.AddOrUpdate(
                "Monthly submission reports",
                () => serviceProvider.GetService<MonthlySubmissionReportsJob>().Send(),
                "0 12 1 * *"
                );
        }
    }
}
