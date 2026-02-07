using System.Globalization;

namespace Weblu.Application.Helpers
{
    public static class DateConverter
    {
        public static string ToShamsi(this DateTimeOffset dateTimeOffset)
        {
            if (dateTimeOffset.UtcTicks <= 0)
            {
                return "";
            }
            DateTime dateTime = dateTimeOffset.DateTime;
            PersianCalendar persianCalendar = new PersianCalendar();
            int year = persianCalendar.GetYear(dateTime);
            int month = persianCalendar.GetMonth(dateTime);
            int day = persianCalendar.GetDayOfMonth(dateTime);
            int hour = persianCalendar.GetHour(dateTime);
            int minute = persianCalendar.GetMinute(dateTime);
            int second = persianCalendar.GetSecond(dateTime);

            return $"{year:D4}/{month:D2}/{day:D2} {hour:D2}:{minute:D2}:{second:D2}";
        }
    }
}