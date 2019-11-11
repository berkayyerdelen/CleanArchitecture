using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Core.Comman.Infrastructure.AutoMapper;
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
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Core.Interface;
using Couchbase.Extensions.Caching;
using Couchbase.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Serilog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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
            services.AddMediatR(typeof(CreateCategoryCommand));
            services.AddMediatR(typeof(UpdateCategoryCommand));
            services.AddConfiguredDbContext(Configuration);
            services.AddScoped<IApplicationDbContext>(s => s.GetService<ApplicationDbContext>());
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            services.AddMvc().AddFluentValidation();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddControllers();

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

            Validations(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API",
                    Description = "Clean Architecture",
                    Contact = new OpenApiContact()
                    {
                        Name = "Berkay Yerdelen",
                        Email = "berkayyerdelen@gmail.com"
                    }
                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement();
                securityRequirement.Add(securitySchema, new[] { "Bearer" });
                c.AddSecurityRequirement(securityRequirement);
            });
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
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        public IServiceCollection Validations(IServiceCollection service)
        {
            service.AddSingleton<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            service.AddSingleton<IValidator<DeleteCategoryCommand>, DeleteCategoryCommandValidator>();
            service.AddSingleton<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>();
            service.AddSingleton<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            service.AddSingleton<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>();
            service.AddSingleton<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            service.AddSingleton<IValidator<CreateOperationClaimCommandHandler>,CreateOperationClaimCommandHandlerValidator>();
            service.AddSingleton<IValidator<DeleteOperationClaimCommandHandler>,DeleteOperationClaimCommandHandlerValidator>();
            service.AddSingleton<IValidator<UpdateOperationClaimCommandHandler>,UpdateOperationClaimHandlerValidator>();
            return service;
        }
    }
}
