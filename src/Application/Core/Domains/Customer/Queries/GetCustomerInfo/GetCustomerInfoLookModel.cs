using MediatR;

namespace Core.Domains.Customer.Queries.GetCustomerInfo
{
    public class GetCustomerInfoLookModel:IRequest
    {
        public int UserId { get; set; }
    }
}