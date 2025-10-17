using System.Collections.Generic;
using PharmacyApp.Common.Common.Errors.Base.Interfaces;
using PharmacyApp.Common.Common.Errors.Base;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace PharmacyApp.Common.Common.Errors.Base.Interfaces
{
    public interface IError
    {
        string Code { get; }
        string Message { get; }
        ErrorType Type { get; }
    }
}
