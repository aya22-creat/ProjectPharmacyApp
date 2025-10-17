using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PharmacyApp.Common.Exceptions;


namespace PharmacyApp.Common.Common.Result
{
    public static class ResultExtensions
    {
        public static void EnsureSuccess(this Result result)
        {
            if (!result.IsSuccess)
            {
                var message = result.Error?.ToString() ?? "Operation failed.";
                throw new OperationFailedException(message);
            }
        }
    }
}