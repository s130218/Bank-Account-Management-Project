using Account.AuthAPI.Models.BankAccount;
using Account.AuthAPI.Service.Common;
using AccountManagement.API.Services.StatementService;
using BankAccountAPI.DTO;
using BankAccountAPI.Model;
using static AccountManagement.API.Enum.TransactionType;

namespace AccountManagement.API.Factory
{
    public class StatementFactory : IStatementFactory
    {
        #region Ctor and Properties
        private readonly IStatementService _statementService;

        public StatementFactory(IStatementService statementService)
        {
            _statementService = statementService;
        }

        public StatementFactory()
        {
            
        }
        #endregion

        public async Task<ServiceResult<Statement>> MapAndAddStatement(StatementDto dto)
        {
            var mappedData = MapToEntity(dto, new Statement());
            var serviceResp = await _statementService.AddStatementAsync(mappedData).ConfigureAwait(false);
            return serviceResp;
        }


        public List<StatementDto> MapAndGetCustomerStatement(List<Statement> entities)
        {
            if (entities == null || entities.Count == 0)
            {
                return new List<StatementDto>();
            }

            var results = new List<StatementDto>();

            foreach (var entity in entities)
            {
                var result = new StatementDto
                {
                    Id = entity.Id,
                    AccountId = entity.AccountId,
                    Amount = entity.Amount,
                    TransactionType = (TransactionTypeEnum)entity.TransactionType,
                    Description = entity.Description
                };
                results.Add(result);
            }

            return results;
        }

        private Statement MapToEntity(StatementDto dto, Statement entity)
        {
            entity.Id = dto.Id;
            entity.AccountId = dto.AccountId;
            entity.Amount = dto.Amount;
            entity.TransactionType = (int)dto.TransactionType; 
            entity.Description = dto.Description;

            return entity;
        }
    }
}
