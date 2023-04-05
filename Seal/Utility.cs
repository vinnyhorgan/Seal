using Raylib_cs;

namespace Seal
{
    public static class Utility
    {
        public static string Clipboard
        {
            get { return Raylib.GetClipboardText_(); }
            set
            {
                Raylib.SetClipboardText(value);

                Logger.Debug("Clipboard set to: " + value);
            }
        }

        public static void TakeScreenshot(string path)
        {
            Raylib.TakeScreenshot(path);

            Logger.Debug("Screenshot taken: " + path);
        }

        // Debug utilities
        internal static void DrawDebug()
        {
            if (Game.Instance.Debug)
            {
                foreach (GameObject gameObject in SceneManager.Instance.CurrentScene.GameObjects)
                {
                    Raylib.DrawRectangle((int)gameObject.Transform.Position.X, (int)gameObject.Transform.Position.Y, 5, 5, Raylib_cs.Color.RED);
                }
            }
        }

        internal static void DrawDebugGUI()
        {
            if (Game.Instance.Debug)
            {
                Raylib.DrawText("FPS: " + Raylib.GetFPS(), 10, 10, 20, Raylib_cs.Color.WHITE);
            }
        }
    }
}
