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

namespace WabApi.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AssignMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(CreateCategoryCommand));
            services.AddMediatR(typeof(UpdateCategoryCommand));
            services.AddMediatR(typeof(DeleteCategoryCommand));
            services.AddMediatR(typeof(FindCategoryByNameQuery));
            services.AddMediatR(typeof(GetCategoryListQuery));

            services.AddMediatR(typeof(CreateCustomerCommandHandler));
            services.AddMediatR(typeof(CheckCustomerExistQuery));
            services.AddMediatR(typeof(CreateAccessTokenQuery));
            services.AddMediatR(typeof(CustomerLoginCheckQuery));

            services.AddMediatR(typeof(FindCustomerByMailQuery));
            services.AddMediatR(typeof(GetCustomerOperationClaimListQuery));

            services.AddMediatR(typeof(CreateOperationClaimCommandHandler));
            services.AddMediatR(typeof(DeleteOperationClaimCommandHandler));
            services.AddMediatR(typeof(UpdateOperationClaimCommandHandler));
            services.AddMediatR(typeof(GetOperationClaimListQuery));

            services.AddMediatR(typeof(CreateProductCommand));
            services.AddMediatR(typeof(DeleteProductCommand));
            services.AddMediatR(typeof(UpdateProductCommand));
            services.AddMediatR(typeof(FindProductByNameQuery));
            services.AddMediatR(typeof(GetProductListQuery));


        }

        public static void Validations(this IServiceCollection services)
        {
            services.AddSingleton<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>();
            services.AddSingleton<IValidator<DeleteCategoryCommand>, DeleteCategoryCommandValidator>();
            services.AddSingleton<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>();
            services.AddSingleton<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddSingleton<IValidator<DeleteProductCommand>, DeleteProductCommandValidator>();
            services.AddSingleton<IValidator<UpdateProductCommand>, UpdateProductCommandValidator>();
            services.AddSingleton<IValidator<CreateOperationClaimCommandHandler>, CreateOperationClaimCommandHandlerValidator>();
            services.AddSingleton<IValidator<DeleteOperationClaimCommandHandler>, DeleteOperationClaimCommandHandlerValidator>();
            services.AddSingleton<IValidator<UpdateOperationClaimCommandHandler>, UpdateOperationClaimHandlerValidator>();
        }
    }
}