using System;

namespace AdbClicker
{
    public class Timestamp
    {
        public static readonly DateTime Default = new DateTime(1970, 1, 1);

        public static double NowUtc
        {
            get
            {
                return DateTime.UtcNow.Subtract(Default).TotalSeconds;
            }
        }

        public static double Now
        {
            get
            {
                return DateTime.Now.Subtract(Default).TotalSeconds;
            }
        }
    }
}
