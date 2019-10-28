using System.Reflection;
using AutoMapper;
using Core.Domains.Category.Commands.CreateCategory;
using Core.Domains.Category.Commands.DeleteCategory;
using Core.Domains.Category.Commands.UpdateCategory;
using Core.Domains.Product.Commands.CreateProduct;
using Core.Domains.Product.Commands.DeleteProduct;
using Core.Domains.Product.Commands.UpdateProduct;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Core.Infrastructure.AutoMapper;
using Core.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;


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
            services.AddConfiguredDbContext(Configuration);
            services.AddScoped<IApplicationDbContext>(s => s.GetService<ApplicationDbContext>());
            services.AddAutoMapper(new Assembly[] { typeof(AutoMapperProfile).GetTypeInfo().Assembly });
            services.AddMvc().AddFluentValidation();
            services.AddControllers();
            Validations(services);
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
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
