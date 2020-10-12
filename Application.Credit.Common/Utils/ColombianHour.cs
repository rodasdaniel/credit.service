using System;

namespace Application.Credit.Common.Utils
{
    public class ColombianHour
    {
        public static DateTime GetDate()
        {
            return DateTime.UtcNow.AddHours(-5);
        }
    }
}
