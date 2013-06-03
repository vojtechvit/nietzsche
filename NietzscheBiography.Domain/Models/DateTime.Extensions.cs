using System;

namespace NietzscheBiography.Domain.Models
{
    public static class DateTimeExtensions
    {
        public static DayTime GetDayTime(this DateTime date)
        {
            if (date.Hour == 0 && date.Minute == 0 && date.Second == 0 && date.Millisecond == 0)
            {
                return DayTime.Midnight;
            }
            if (date.Hour > 0 && date.Hour < 6)
            {
                return DayTime.EarlyMorning;
            }
            else if (date.Hour >= 6 && date.Hour < 12)
            {
                return DayTime.Morning;
            }
            else if (date.Hour == 12 && date.Minute == 0 && date.Second == 0 && date.Millisecond == 0)
            {
                return DayTime.Noon;
            }
            else if (date.Hour > 12 && date.Hour < 18)
            {
                return DayTime.Afternoon;
            }
            else if (date.Hour >= 18 && date.Hour < 20)
            {
                return DayTime.Evening;
            }
            else
            {
                return DayTime.Night;
            }
        }

        public static Season GetSeason(this DateTime date)
        {
            var m = date.Month * 100;
            var d = date.Day;
            var md = m + d;

            if ((md > 320) && (md < 621))
            {
                return Season.Spring;
            }
            else if ((md > 620) && (md < 923))
            {
                return Season.Summer;
            }
            else if ((md > 922) && (md < 1223))
            {
                return Season.Autumn;
            }
            else
            {
                return Season.Winter;
            }
        }

        public static byte GetDecade(this DateTime date)
        {
            // 702, 1984 -> 02, 84 -> 0.2, 8.4 -> 0, 8
            return (byte)Math.Floor((decimal)(date.Year % 100 / 10));
        }

        public static int GetCentury(this DateTime date)
        {
            // 702, 1984 -> 7.02, 19.84 -> 7, 19
            return (byte)Math.Floor((decimal)(date.Year / 100));
        }
    }
}