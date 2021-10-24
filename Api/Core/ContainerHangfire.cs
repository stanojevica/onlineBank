using Api.Core.BackgroungJobs;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core
{
    public static class ContainerHangfire
    {
        public static void AddHungfire(this IServiceCollection services)
        {
            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();

            services.AddTransient<MonthlyMaintenanceJob>();
            services.AddTransient<MonthlyPaymentJob>();
            services.AddTransient<MonthlySubmissionReportsJob>();
        }

    }
}
