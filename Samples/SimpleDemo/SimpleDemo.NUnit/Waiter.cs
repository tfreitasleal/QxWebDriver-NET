namespace SimpleDemo.NUnit
{
    public static class Waiter
    {
#if NOWAIT
        public static int Duration = 0;
#elif DEBUG
        public static int Duration = 2000;
#else
        public static int Duration = 500;
#endif
    }
}