using System;
using System.Collections.Generic;
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
using Couchbase.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
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

        public static void Validations(this IServiceCollection service)
        {
            service.AddSingleton<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            service.AddSingleton<IValidator<DeleteCategoryCommand>, DeleteCategoryCommandValidator>();
            service.AddSingleton<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>();
            service.AddSingleton<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            service.AddSingleton<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>();
            service.AddSingleton<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            service.AddSingleton<IValidator<CreateOperationClaimCommandHandler>, CreateOperationClaimCommandHandlerValidator>();
            service.AddSingleton<IValidator<DeleteOperationClaimCommandHandler>, DeleteOperationClaimCommandHandlerValidator>();
            service.AddSingleton<IValidator<UpdateOperationClaimCommandHandler>, UpdateOperationClaimHandlerValidator>();
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