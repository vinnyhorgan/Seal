using System;
using System.Numerics;
using ImGuiNET;
using Raylib_cs;

namespace Seal
{
    public struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }

    public class Game : IDisposable
    {
        public static Game Instance;

        private Settings _settings;
        private string _title;
        private int _targetFps;
        private Color _clearColor;
        private bool _quit;
        private Vector2 _virtualMouse;
        private RenderTexture2D _target;
        private ImguiController _imgui;

        public Game() : this(new Settings())
        {
        }

        public Game(Settings settings)
        {
            _settings = settings;
            _title = settings.Title;
            _targetFps = settings.TargetFps;
            _clearColor = new Color { R = 0, G = 0, B = 0, A = 255 };
            _quit = false;
            _virtualMouse = new Vector2(0, 0);

            Debug = _settings.Debug;

            Logger.Init();
            Logger.DebugEnabled = Debug;

            Instance = this;
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Letterbox { get; private set; }

        public string Title
        {
            get { return _title; }
            set
            {
                Raylib.SetWindowTitle(_title);
                _title = value;

                Logger.Debug("Window title set to " + value);
            }
        }

        public int TargetFps
        {
            get { return _targetFps; }
            set
            {
                Raylib.SetTargetFPS(_targetFps);
                _targetFps = value;

                Logger.Debug("Target Fps set to " + value);
            }
        }

        public bool Fullscreen
        {
            get { return Raylib.IsWindowFullscreen(); }
            set
            {
                if (value && !Raylib.IsWindowFullscreen())
                {
                    Raylib.ToggleFullscreen();
                }
                else if (!value && Raylib.IsWindowFullscreen())
                {
                    Raylib.ToggleFullscreen();
                }

                Logger.Debug("Fullscreen set to " + value);
            }
        }

        public bool Resizable
        {
            get { return Raylib.IsWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE); }
            set
            {
                if (value && !Raylib.IsWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE))
                {
                    Raylib.SetWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
                }
                else if (!value && Raylib.IsWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE))
                {
                    Raylib.ClearWindowState(ConfigFlags.FLAG_WINDOW_RESIZABLE);
                }

                Logger.Debug("Resizable set to " + value);
            }
        }

        public bool VSync
        {
            get { return Raylib.IsWindowState(ConfigFlags.FLAG_VSYNC_HINT); }
            set
            {
                if (value && !Raylib.IsWindowState(ConfigFlags.FLAG_VSYNC_HINT))
                {
                    Raylib.SetWindowState(ConfigFlags.FLAG_VSYNC_HINT);
                }
                else if (!value && Raylib.IsWindowState(ConfigFlags.FLAG_VSYNC_HINT))
                {
                    Raylib.ClearWindowState(ConfigFlags.FLAG_VSYNC_HINT);
                }

                Logger.Debug("VSync set to " + value);
            }
        }

        public bool Debug
        {
            get { return Logger.DebugEnabled; }
            set { Logger.DebugEnabled = value; }
        }

        public Color ClearColor
        {
            get { return _clearColor; }
            set
            {
                _clearColor = value;

                Logger.Debug("Clear color set to " + value);
            }
        }

        internal Vector2 VirtualMouse { get { return _virtualMouse; } }

        public void Dispose()
        {
            if (_imgui != null)
            {
                _imgui.Dispose();
            }

            AssetManager.Instance.ClearAssets();

            if (Letterbox)
            {
                Raylib.UnloadRenderTexture(_target);
            }

            if (Raylib.IsAudioDeviceReady())
            {
                Raylib.CloseAudioDevice();
            }

            if (Raylib.IsWindowReady())
            {
                Raylib.CloseWindow();
            }
        }

        public void Quit()
        {
            _quit = true;

            Logger.Debug("Requested quit");
        }

        public void Run()
        {
            if (SceneManager.Instance.CurrentScene == null)
            {
                Logger.Fatal("No scene set");

                return;
            }

            Letterbox = _settings.Letterbox;

            Width = _settings.Width;
            Height = _settings.Height;

            Raylib.InitWindow(Width, Height, Title);
            Raylib.SetWindowMinSize(Width / 2, Height / 2);

            TargetFps = _settings.TargetFps;
            Resizable = _settings.Resizable;
            VSync = _settings.VSync;
            Fullscreen = _settings.Fullscreen;

            Raylib.InitAudioDevice();

            if (Letterbox)
            {
                _target = Raylib.LoadRenderTexture(Width, Height);
                Raylib.SetTextureFilter(_target.texture, TextureFilter.TEXTURE_FILTER_POINT);
            }

            _imgui = new ImguiController();
            _imgui.Load(Width, Height);

            AssetManager.Instance.Init();

            while (!_quit)
            {
                if (Raylib.WindowShouldClose())
                {
                    _quit = true;
                }

                GameObject.Update();

                AssetManager.Instance.Update();

                float dt = Raylib.GetFrameTime();

                float scale = Math.Min((float)Raylib.GetScreenWidth() / Width, (float)Raylib.GetScreenHeight() / Height);

                Vector2 mouse = Raylib.GetMousePosition();
                _virtualMouse = Vector2.Zero;
                _virtualMouse.X = (mouse.X - (Raylib.GetScreenWidth() - (Width * scale)) * 0.5f) / scale;
                _virtualMouse.Y = (mouse.Y - (Raylib.GetScreenHeight() - (Height * scale)) * 0.5f) / scale;

                Vector2 max = new Vector2((float)Width, (float)Height);
                _virtualMouse = Vector2.Clamp(_virtualMouse, Vector2.Zero, max);

                _imgui.Update(dt);

                Raylib.BeginDrawing();

                Raylib.ClearBackground(new Raylib_cs.Color(_clearColor.R, _clearColor.G, _clearColor.B, _clearColor.A));

                if (Letterbox)
                {
                    Raylib.BeginTextureMode(_target);

                    Raylib.ClearBackground(new Raylib_cs.Color(_clearColor.R, _clearColor.G, _clearColor.B, _clearColor.A));

                    Raylib.BeginMode2D(SceneManager.Instance.CurrentScene.Camera.Camera2D);

                    SceneManager.Instance.CurrentScene.OnUpdate();

                    Utility.DrawDebug();

                    Raylib.EndMode2D();

                    Raylib.EndTextureMode();

                    Rectangle sourceRec = new Rectangle(
                        0.0f,
                        0.0f,
                        (float)_target.texture.width,
                        (float)-_target.texture.height
                    );

                    Rectangle destRec = new Rectangle(
                        (Raylib.GetScreenWidth() - ((float)Width * scale)) * 0.5f,
                        (Raylib.GetScreenHeight() - ((float)Height * scale)) * 0.5f,
                        (float)Width * scale,
                        (float)Height * scale
                    );

                    Raylib.DrawTexturePro(_target.texture, sourceRec, destRec, new Vector2(0, 0), 0.0f, Raylib_cs.Color.WHITE);
                }
                else
                {
                    Raylib.BeginMode2D(SceneManager.Instance.CurrentScene.Camera.Camera2D);

                    SceneManager.Instance.CurrentScene.OnUpdate();

                    Utility.DrawDebug();

                    Raylib.EndMode2D();
                }

                SceneManager.Instance.CurrentScene.OnGUI();

                Utility.DrawDebugGUI();

                _imgui.Draw();

                Raylib.EndDrawing();
            }
        }
    }
}
