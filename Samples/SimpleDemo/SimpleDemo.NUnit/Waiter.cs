namespace SimpleDemo.NUnit
{
    public static class Waiter
    {
#if DEBUG
        public static int Duration = 1000;
#else
        public static int Duration = 0;
#endif
    }
}