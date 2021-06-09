namespace FixedMath.NET
{
    public class Constants
    {
        public const long MAX_VALUE = long.MaxValue;
        public const long MIN_VALUE = long.MinValue;
        public const int NUM_BITS = 64;
        public const int FRACTIONAL_PLACES = 32;
        public const long ONE = 1L << FRACTIONAL_PLACES;
        public const long PI_TIMES_2 = 0x6487ED511;
        public const long PI = 0x3243F6A88;
        public const long PI_OVER_2 = 0x1921FB544;
        public const long LN2 = 0xB17217F7;
        public const long LOG2MAX = 0x1F00000000;
        public const long LOG2MIN = -0x2000000000;
        public const int LUT_SIZE = (int)(PI_OVER_2 >> 15);
    }
}