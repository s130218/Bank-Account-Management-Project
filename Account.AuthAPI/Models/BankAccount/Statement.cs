using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccountAPI.Model
{
    [Table("Statement")]

    public class Statement
    {
        public int Id { get; set; }

        public int AccountId {  get; set; }

        public double Amount { get; set; }

        public int TransactionType {  get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public Guid? CreatedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public Guid? ModifiedBy { get; set; }
    }
}
