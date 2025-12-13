using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Application.Services.Interfaces.Humans.User
{
    public interface ICurrentUserService
    {
        int? GetCurrentUserId();
    }
}
