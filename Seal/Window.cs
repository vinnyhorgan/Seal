using Raylib_cs;
using System.Numerics;

namespace Seal
{
    public static class Window
    {
        public static bool Ready
        {
            get { return Raylib.IsWindowReady(); }
        }

        public static Vector2 Position
        {
            get { return Raylib.GetWindowPosition(); }
            set
            {
                Raylib.SetWindowPosition((int)value.X, (int)value.Y);

                Logger.Debug("Window position set to " + value);
            }
        }

        public static Vector2 Size
        {
            get { return new Vector2(Raylib.GetScreenWidth(), Raylib.GetScreenHeight()); }
            set
            {
                Raylib.SetWindowSize((int)value.X, (int)value.Y);

                Logger.Debug("Window size set to " + value);
            }
        }

        public static Vector2 MinSize
        {
            set
            {
                Raylib.SetWindowMinSize((int)value.X, (int)value.Y);

                Logger.Debug("Window minimum size set to " + value);
            }
        }

        public static bool Maximized
        {
            get { return Raylib.IsWindowMaximized(); }
            set
            {
                if (value && !Raylib.IsWindowMaximized())
                {
                    Raylib.MaximizeWindow();
                }
                else if (!value && Raylib.IsWindowMaximized())
                {
                    Raylib.RestoreWindow();
                }

                Logger.Debug("Window maximized set to " + value);
            }
        }

        public static bool Minimized
        {
            get { return Raylib.IsWindowMinimized(); }
            set
            {
                if (value && !Raylib.IsWindowMinimized())
                {
                    Raylib.MinimizeWindow();
                }
                else if (!value && Raylib.IsWindowMinimized())
                {
                    Raylib.RestoreWindow();
                }

                Logger.Debug("Window minimized set to " + value);
            }
        }

        public static bool Focused { get { return Raylib.IsWindowFocused(); } }
        public static bool Hidden { get { return Raylib.IsWindowHidden(); } }

        public static void SetIcon(string path)
        {
            Raylib.SetWindowIcon(Raylib.LoadImage(path));

            Logger.Debug("Window icon set to " + path);
        }
    }
}
