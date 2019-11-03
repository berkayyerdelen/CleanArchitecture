using System.Reflection;
using AutoMapper;
using Core.Comman.Infrastructure.AutoMapper;
using Core.Domains.Category.Commands.CreateCategory;
using Core.Domains.Category.Commands.DeleteCategory;
using Core.Domains.Category.Commands.UpdateCategory;
using Core.Domains.Product.Commands.CreateProduct;
using Core.Domains.Product.Commands.DeleteProduct;
using Core.Domains.Product.Commands.UpdateProduct;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Core.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Serilog;


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
            services.AddControllers();
            Validations(services);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
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

            app.UseHttpsRedirection();
            app.UseSerilogRequestLogging();
            app.UseRouting();

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
            return service;
        }
    }
}
