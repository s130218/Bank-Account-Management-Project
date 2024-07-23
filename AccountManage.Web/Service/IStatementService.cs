using AccountManagement.Web.Models.DTO.Common;
using AccountManagement.Web.Models.DTO.Statement;

namespace AccountManagement.Web.Service
{
    public interface IStatementService
    {
        Task<ServiceResult<object>> GetAllStatementAsync();

        Task<ServiceResult> AddTranscationAsync(StatementDto account);
    }
}
