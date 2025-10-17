
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using PharmacyApp.Common.Common.Errors.Base.Interfaces;

namespace PharmacyApp.Common.Common.Errors.Base
{

    public abstract class ErrorBase : IError
    {
        public string Code { get; protected set; } = string.Empty;
        public string Message { get; protected set; } = string.Empty;
         public ErrorType Type { get; protected set; } = ErrorType.Unexpected;
        

   
        protected ErrorBase(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString() => $"{Code}: {Message}";
    }
}
