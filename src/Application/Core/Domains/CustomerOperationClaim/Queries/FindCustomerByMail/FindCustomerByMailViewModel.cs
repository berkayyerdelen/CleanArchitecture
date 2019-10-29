namespace Core.Domains.CustomerOperationClaim.Queries.FindCustomerByMail
{
    public class FindCustomerByMailViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
    }
}