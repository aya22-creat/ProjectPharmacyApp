using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Common.Domain.Errors.Base;
using PharmacyApp.Common.Domain.Errors.Base.Interfaces;

using PharmacyApp.Common.Domain.Errors.Base;

namespace PharmacyApp.Common.Domain.Errors
{
    public sealed class DomainError : ErrorBase
    {
        private DomainError(string code, string message)
            : base(code, message) { }

        public static DomainError Validation(string message)
            => new DomainError("Domain.Validation", message);

        public static DomainError NotFound(string entity, string id)
            => new DomainError("Domain.NotFound", $"{entity} with ID {id} was not found.");

        public static DomainError Conflict(string message)
            => new DomainError("Domain.Conflict", message);
    }
}
