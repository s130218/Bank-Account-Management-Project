using Account.AuthAPI.Service.Common;
using BankAccountAPI.DTO;
using BankAccountAPI.Model;

namespace AccountManagement.API.Factory
{
    public interface IStatementFactory
    {
        ServiceResult<List<StatementDto>> MapAndGetCustomerStatement(List<Statement> entities);
        Task<ServiceResult<Statement>> MapAndAddStatement(StatementDto dto);
    }
}
