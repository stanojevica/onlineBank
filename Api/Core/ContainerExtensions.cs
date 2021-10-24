using Api.Core.Jwt;
using Application;
using Application.Interfaces;
using DataAccess;
using Implementation.Commands.Contact;
using Implementation.Commands.Credits;
using Implementation.Commands.Packages;
using Implementation.Commands.Users;
using Implementation.Commands.Users.Credits;
using Implementation.Commands.Users.Transactions;
using Implementation.Queries;
using Implementation.Validators.Contact;
using Implementation.Validators.Credit;
using Implementation.Validators.Packages;
using Implementation.Validators.Users;
using Implementation.Validators.Users.Credits;
using Implementation.Validators.Users.Transactions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Core
{
    public static class ContainerExtensions
    {
        public static void AddUsesCases(this IServiceCollection services)
        {
            services.AddTransient<UseCaseExecutor>();

            //queries
            services.AddTransient<GetCreditQuery>();
            services.AddTransient<GetPackageQuery>();
            services.AddTransient<GetUserQuery>();
            services.AddTransient<GetTransactionQuery>();
            services.AddTransient<GetStatusQuery>();

            //package commands
            services.AddTransient<CreatePackageCommand>();
            services.AddTransient<UpdatePackageCommand>();
            services.AddTransient<DeletePackageCommand>();
            services.AddTransient<DeactivatePackageCommand>();

            //package validators
            services.AddTransient<CreatePackageValidator>();
            services.AddTransient<UpdatePackageValidator>();

            //user commands
            services.AddTransient<CreateUserCommand>();
            services.AddTransient<UpdateUserCommand>();
            services.AddTransient<UpdatePasswordCommand>();
            services.AddTransient<DeleteUserCommand>();
            services.AddTransient<DeactivateUserCommand>();
            //credit users command
            services.AddTransient<RequestCreditCommand>();
            services.AddTransient<UpdateCreditStatusCommand>();
            //transaction user command
            services.AddTransient<TransactionCommand>();

            //user validators
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserValidator>();
            services.AddTransient<PasswordValidator>();
            //credit user validators
            services.AddTransient<RequestCreditValidator>();
            services.AddTransient<UpdateCreditStatusValidator>();
            //transaction user validators
            services.AddTransient<TransactionValidator>();

            //credit commands
            services.AddTransient<CreateCreditCommand>();
            services.AddTransient<UpdateCreditCommand>();
            services.AddTransient<DeleteCreditCommand>();
            services.AddTransient<DeactivateCreditCommand>();

            //credit validators
            services.AddTransient<CreateCreditValidator>();
            services.AddTransient<UpdateCreditValidator>();

            //contact command
            services.AddTransient<ContactCommand>();

            //contact validator
            services.AddTransient<ContactValidator>();
        }

        public static void AddApplicationUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var user = accessor.HttpContext.User;

                if (user.FindFirst("UserData") == null)
                {
                    return new UnregistredUser();
                }

                var userString = user.FindFirst("UserData").Value;

                var userJwt = JsonConvert.DeserializeObject<JwtUser>(userString);

                return userJwt;
            });
        }

        public static void AddJwt(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<Context>();

                return new JwtManager(context, appSettings.JwtIssuer, appSettings.JwtSecretKey);
            });
            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = appSettings.JwtIssuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
