using Raylib_cs;

namespace Seal
{
    public static class Time
    {
        public static float DeltaTime
        {
            get { return Raylib.GetFrameTime(); }
        }

        public static float TimeSinceStartup
        {
            get { return (float)Raylib.GetTime(); }
        }

        public static float FPS
        {
            get { return Raylib.GetFPS(); }
        }
    }
}
