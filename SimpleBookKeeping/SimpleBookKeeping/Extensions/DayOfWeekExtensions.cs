
using System;

namespace SimpleBookKeeping.Extensions
{
    public static class DayOfWeekExtensions
    {
        public static string ToShortRuString(this DayOfWeek dayOfWeek)
        {
            string day;
            switch (dayOfWeek)
            {
                case DayOfWeek.Sunday:
                    day = "Вс";
                    break;
                case DayOfWeek.Monday:
                    day = "Пн";
                    break;
                case DayOfWeek.Tuesday:
                    day = "Вт";
                    break;
                case DayOfWeek.Wednesday:
                    day = "Ср";
                    break;
                case DayOfWeek.Thursday:
                    day = "Чт";
                    break;
                case DayOfWeek.Friday:
                    day = "Пт";
                    break;
                case DayOfWeek.Saturday:
                    day = "Сб";
                    break;
                default:
                    day = "Хз";
                    break;
            }
            return day;
        }

    }
}