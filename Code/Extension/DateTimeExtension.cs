using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Extension
{
    public static class DateTimeExtension
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        public static string CurrentDay(DateTime yourDate)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - yourDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "Một giây trước" : ts.Seconds + " giây trước";

            if (delta < 2 * MINUTE)
                return "Một phút trước";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " phút trước";

            if (delta < 90 * MINUTE)
                return "một giờ trước";

            if (delta < 24 * HOUR)
                return ts.Hours + " giờ trước";

            if (delta < 48 * HOUR)
                return "hôm qua";

            if (delta < 30 * DAY)
                return ts.Days + " ngày trước";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "một tháng trước" : months + " tháng trước";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "một năm trước" : years + " năm trước";
            }
        }

        public static string WatingDay(DateTime yourDate)
        {
            var ts = new TimeSpan(DateTime.Now.Ticks - yourDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "Một giây" : Math.Abs(ts.Seconds) + " giây";

            if (delta < 2 * MINUTE)
                return "Một phút";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " phút";

            if (delta < 90 * MINUTE)
                return "một giờ";

            if (delta < 24 * HOUR)
                return ts.Hours + " giờ còn lại";

            if (delta < 48 * HOUR)
                return "hôm còn lại";

            if (delta < 30 * DAY)
                return ts.Days + " ngày còn lại";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "một tháng còn lại" : months + " tháng còn lại";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "một năm còn lại" : years + " năm còn lại";
            }
        }
    }
}
