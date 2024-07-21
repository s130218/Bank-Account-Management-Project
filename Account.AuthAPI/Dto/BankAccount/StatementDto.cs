using AccountManagement.API.Enum;
using static AccountManagement.API.Enum.TransactionType;

namespace BankAccountAPI.DTO
{
    public class StatementDto
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public double Amount { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public string? Description { get; set; }
    }
}
