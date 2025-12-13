using DVLD_Application.Results;
using DVLD_Application.Services.Interfaces.Humans.User;
using DVLD_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_E_Enfrastructure.Service.Implementaions.Humans.User
{
    public class UserLoginAuditService : IUserLoginAuditService
    {
        public Task<Result<bool>> LogUserLoginAsync(UserLoginAudit userLoginAudit)
        {
            throw new NotImplementedException();
        }
    }
}
