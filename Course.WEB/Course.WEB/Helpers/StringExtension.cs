using System;

namespace Course.WEB.Helpers
{
    public static class StringExtensions
    {
        public static int ConvertStringTimeToInt(this string time)
        {
            string[] substrings = time.Split(':');
            int sumTime = Convert.ToInt32(substrings[0]) * 3600;
            sumTime += Convert.ToInt32(substrings[1]) * 60;
            sumTime += Convert.ToInt32(substrings[2].Split('.')[0]);
            return sumTime;
        }
    }
}