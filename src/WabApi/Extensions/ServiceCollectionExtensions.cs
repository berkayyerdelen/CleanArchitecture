using Core.Domains.Category.Commands.CreateCategory;
using Core.Domains.Category.Commands.DeleteCategory;
using Core.Domains.Category.Commands.UpdateCategory;
using Core.Domains.Category.Queries.FindCategoryByName;
using Core.Domains.Category.Queries.GetCategoryList;
using Core.Domains.Customer.Commands.CreateCustomer;
using Core.Domains.Customer.Queries.CheckCustomerExist;
using Core.Domains.Customer.Queries.CreateAccessToken;
using Core.Domains.Customer.Queries.LoginCheckCustomer;
using Core.Domains.CustomerOperationClaim.Queries.FindCustomerByMail;
using Core.Domains.CustomerOperationClaim.Queries.GetCustomerOperationClaims;
using Core.Domains.OperationClaim.Command.CreateOperationClaim;
using Core.Domains.OperationClaim.Command.DeleteOperationClaim;
using Core.Domains.OperationClaim.Command.UpdateOperationClaim;
using Core.Domains.OperationClaim.Queries;
using Core.Domains.Product.Commands.CreateProduct;
using Core.Domains.Product.Commands.DeleteProduct;
using Core.Domains.Product.Commands.UpdateProduct;
using Core.Domains.Product.Queries.FindProductByName;
using Core.Domains.Product.Queries.GetProductList;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;

namespace WabApi.Extensions
{
    public static class ServiceCollectionExtensions
    { 
        public static void AssignMediatr(this IServiceCollection service)
        {
            service.AddMediatR(typeof(CreateCategoryCommand));
            service.AddMediatR(typeof(UpdateCategoryCommand));
            service.AddMediatR(typeof(DeleteCategoryCommand));
            service.AddMediatR(typeof(FindCategoryByNameQuery));
            service.AddMediatR(typeof(GetCategoryListQuery));

            service.AddMediatR(typeof(CreateCustomerCommandHandler));
            service.AddMediatR(typeof(CheckCustomerExistQuery));
            service.AddMediatR(typeof(CreateAccessTokenQuery));
            service.AddMediatR(typeof(CustomerLoginCheckQuery));

            service.AddMediatR(typeof(FindCustomerByMailQuery));
            service.AddMediatR(typeof(GetCustomerOperationClaimListQuery));

            service.AddMediatR(typeof(CreateOperationClaimCommandHandler));
            service.AddMediatR(typeof(DeleteOperationClaimCommandHandler));
            service.AddMediatR(typeof(UpdateOperationClaimCommandHandler));
            service.AddMediatR(typeof(GetOperationClaimListQuery));

            service.AddMediatR(typeof(CreateProductCommand));
            service.AddMediatR(typeof(DeleteProductCommand));
            service.AddMediatR(typeof(UpdateProductCommand));
            service.AddMediatR(typeof(FindProductByNameQuery));
            service.AddMediatR(typeof(GetProductListQuery));
        }

        public static void ConfigureValidations(this IServiceCollection service)
        {
            service.TryAddEnumerable(new []
            {
            ServiceDescriptor.Scoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>(),
            ServiceDescriptor.Scoped<IValidator<DeleteCategoryCommand>, DeleteCategoryCommandValidator>(),
            ServiceDescriptor.Scoped<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>(),
            ServiceDescriptor.Scoped<IValidator<CreateProductCommand>, CreateProductCommandValidator>(),
            ServiceDescriptor.Scoped<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>(),
            ServiceDescriptor.Scoped<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>(),
            ServiceDescriptor.Scoped<IValidator<CreateOperationClaimCommandHandler>, CreateOperationClaimCommandHandlerValidator>(),
            ServiceDescriptor.Scoped<IValidator<DeleteOperationClaimCommandHandler>, DeleteOperationClaimCommandHandlerValidator>(),
            ServiceDescriptor.Scoped<IValidator<UpdateOperationClaimCommandHandler>, UpdateOperationClaimHandlerValidator>(),
            });
        }

      
        public static void AddSwagger(this IServiceCollection service)
        {
            service.AddSwaggerGen(c =>
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

       
    }
}