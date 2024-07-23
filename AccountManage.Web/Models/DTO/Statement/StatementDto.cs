using AccountManage.Web.Enum;

namespace AccountManagement.Web.Models.DTO.Statement
{
    public class StatementDto
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public double Amount { get; set; }

        public TransactionTypeEnum TransactionType { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
