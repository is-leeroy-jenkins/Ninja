using System;

namespace Ninja.Utilities
{
    public static class TimestampHelper
    {
        public static string GetTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmss");
        }
    }
}