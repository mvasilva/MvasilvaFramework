using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mvasilva.Framework.BLL.Util
{
    public static class DatetimeExtensions
    {

        public static DateTime GetFirstDayMonth(this DateTime dt)
        {


            return new DateTime(dt.Year, dt.Month, 1);
        }


        public static int GetWeekOfYear(this DateTime data, DayOfWeek diaSemanaReferencia)
        {
            System.Globalization.GregorianCalendar gc = new System.Globalization.GregorianCalendar();

            return gc.GetWeekOfYear(data, System.Globalization.CalendarWeekRule.FirstFullWeek, diaSemanaReferencia);

        }

        public static DateTime GetBeginWeekDay(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

    }
}
