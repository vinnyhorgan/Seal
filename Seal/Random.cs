using Raylib_cs;

namespace Seal
{
    public static class Random
    {
        public static int State
        {
            set
            {
                Raylib.SetRandomSeed((uint)value);

                Logger.Debug("Random seed set to " + value);
            }
        }

        public static float Value
        {
            get { return Raylib.GetRandomValue(0, 100) / 100.0f; }
        }

        public static float Range(int min, int max)
        {
            return Raylib.GetRandomValue(min, max);
        }
    }
}
