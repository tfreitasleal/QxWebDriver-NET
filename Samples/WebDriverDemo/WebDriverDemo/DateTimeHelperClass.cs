﻿//---------------------------------------------------------------------------------------------------------
//    Copyright © 2007 - 2017 Tangible Software Solutions Inc.
//    This class can be used by anyone provided that the copyright notice remains intact.
//
//    This class is used to replace calls to Java's System.currentTimeMillis with the C# equivalent.
//    Unix time is defined as the number of seconds that have elapsed since midnight UTC, 1 January 1970.
//---------------------------------------------------------------------------------------------------------

namespace Qooxdoo.WebDriverDemo
{
    internal static class DateTimeHelperClass
    {
        private static readonly System.DateTime Jan1St1970 = new System.DateTime(1970, 1, 1, 0, 0, 0, System.DateTimeKind.Utc);

        internal static long CurrentUnixTimeMillis()
        {
            return (long) (System.DateTime.UtcNow - Jan1St1970).TotalMilliseconds;
        }
    }
}