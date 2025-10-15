using System.Collections.Generic;
using PharmacyApp.Common.Domain.Errors.Base.Interfaces;
using PharmacyApp.Common.Domain.Errors.Base;
using PharmacyApp.Common.Domain.Errors;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Common.Domain.Errors;



namespace PharmacyApp.Common.Domain.Errors.Base.Interfaces
{
    public interface IError
    {
        string Code { get; }
        string Message { get; }
    }
}
