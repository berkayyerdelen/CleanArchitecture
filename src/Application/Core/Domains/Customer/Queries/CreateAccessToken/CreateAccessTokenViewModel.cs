using System;

namespace Core.Domains.Customer.Queries.CreateAccessToken
{
    public class CreateAccessTokenViewModel
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}