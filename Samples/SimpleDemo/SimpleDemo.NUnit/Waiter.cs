namespace SimpleDemo.NUnit
{
    public static class Waiter
    {
#if DEBUG
        public static int Duration = 2000;
#else
        public static int Duration = 200;
#endif
    }
}