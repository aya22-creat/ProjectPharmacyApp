
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using PharmacyApp.Common.Domain.Errors.Base.Interfaces;

namespace PharmacyApp.Common.Domain.Errors.Base
{
    public abstract class ErrorBase : IError
    {
        public string Code { get; protected set; }
        public string Message { get; protected set; }

        protected ErrorBase(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public override string ToString() => $"{Code}: {Message}";
    }
}
