﻿namespace SimpleDemo.NUnit
{
    public static class Waiter
    {
#if NOWAIT
        public static int Duration = 0;
#elif RELEASE
        public static int Duration = 500;
#else
        public static int Duration = 2000;
#endif
    }
}