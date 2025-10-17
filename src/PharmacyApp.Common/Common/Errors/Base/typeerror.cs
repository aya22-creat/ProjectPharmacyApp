using PharmacyApp.Common.Common.Errors.Base;
using PharmacyApp.Common.Common.Errors.Base.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PharmacyApp.Common.Common.Errors.Base
{
    public enum ErrorType
    {
        Unexpected,
        Validation,
        NotFound,
        Conflict,
        Unauthorized,
        Forbidden,
        Failure,
        None
    }
}
