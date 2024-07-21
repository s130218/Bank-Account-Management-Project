namespace AccountManagement.Web.Models.DTO.Account
{
    public class AccountDto
    {
        public int Id { get; set; }

        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public decimal TotalAmount { get; set; }

        public bool Status { get; set; }
    }
}
