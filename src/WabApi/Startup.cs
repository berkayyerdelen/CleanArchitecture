using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Core.Comman.Infrastructure;
using Core.Comman.Infrastructure.AutoMapper;
using Core.Comman.Interface.AppUserSession;
using Core.Comman.Security.Encryption;
using Core.Comman.Security.Jwt;
using Core.Domains.Category.Commands.CreateCategory;
using Core.Domains.Category.Commands.DeleteCategory;
using Core.Domains.Category.Commands.UpdateCategory;
using Core.Domains.OperationClaim.Command.CreateOperationClaim;
using Core.Domains.OperationClaim.Command.DeleteOperationClaim;
using Core.Domains.OperationClaim.Command.UpdateOperationClaim;
using Core.Domains.Product.Commands.CreateProduct;
using Core.Domains.Product.Commands.DeleteProduct;
using Core.Domains.Product.Commands.UpdateProduct;
using FluentValidation.AspNetCore;
using Core.Interface;
using Couchbase.Extensions.Caching;
using Couchbase.Extensions.DependencyInjection;
using FluentValidation;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WabApi.Extensions;

namespace WabApi
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
            
            services.AssignMediatr();
            services.ConfigureValidations();
            services.AddConfiguredDbContext(Configuration);
            services.AddScoped<IApplicationDbContext>(s => s.GetService<ApplicationDbContext>());
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            services.AddMvc().AddFluentValidation();
            services.AddScoped<IAppUserIdSession, AppUserIdSession>();
            services.AddScoped<ITokenHelper, JwtHelper>();
           
            services.AddControllers().AddControllersAsServices();


            services.AddHangfire(_ => _.UseSqlServerStorage(Configuration.GetValue<string>("HangfireDbConn")));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigin",builder=>builder.WithOrigins("http://localhost:3000"));
            });
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                options =>
                {
                    options.TokenValidationParameters= new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });

            services.AddCouchbase(opt =>
            {
                opt.Servers= new List<Uri>()
                {
                    new Uri("http://localhost:8091")
                };
                opt.Username = Configuration.GetValue<string>("Couchbase:ClusterId");
                opt.Password = Configuration.GetValue<string>("Couchbase:ClusterPassword");
                opt.UseSsl = false;
            });
            services.AddDistributedCouchbaseCache(Configuration.GetValue<string>("Couchbase:DistributedCouchbaseCache"),opt => { });
            services.AddSwagger();
          

        }

      
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builders => builders.WithOrigins("http://localhost:3000").AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
           
            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            
            app.ApplicationBuilder();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
      
    }
}
