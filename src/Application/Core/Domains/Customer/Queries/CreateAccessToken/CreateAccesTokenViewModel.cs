using System;
using MediatR;

namespace Core.Domains.Customer.Queries.CreateAccessToken
{
    public class CreateAccesTokenViewModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}