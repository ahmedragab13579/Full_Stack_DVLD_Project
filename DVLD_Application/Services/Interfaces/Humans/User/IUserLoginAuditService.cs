using DVLD_Application.Results;
using DVLD_Domain.Models;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Humans.User
{
    public interface IUserLoginAuditService
    {
        Task<Result<bool>> LogUserLoginAsync(UserLoginAudit userLoginAudit);
    }
}
