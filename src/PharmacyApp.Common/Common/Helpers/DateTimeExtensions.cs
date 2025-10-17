using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    

namespace PharmacyApp.Common.Helpers
{
    public static class DateTimeExtensions
    {
        public static bool IsExpired(this DateTime dateTime) => dateTime < DateTime.UtcNow;
    }
}
