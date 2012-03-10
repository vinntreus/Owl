using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public static class DateTimeExtensions
    {
        public static string ToDateString(this DateTime datetime)
        {
            return datetime.ToString("yyy-MM-dd HH:mm:ss");
        }
    }
}
