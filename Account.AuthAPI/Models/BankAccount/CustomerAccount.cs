using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations.Schema;

namespace Account.AuthAPI.Models.BankAccount
{
    [Table("Account")]

    public class CustomerAccount
    {
        public int Id { get; set; }

        public string UserId {  get; set; }
        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public decimal TotalAmount { get; set; }

        public bool Status { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}
