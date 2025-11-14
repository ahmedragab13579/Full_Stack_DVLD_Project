using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVDL_Domain.Enums
{
    public enum LicenseIssueReason
    {
        FirstTime = 1,
        Renewal = 2,
        ReplacementForLost = 3,
        ReplacementForDamaged = 4
    }
}
