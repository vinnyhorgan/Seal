using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;

namespace Seal
{
    public enum KeyboardKey
    {
        KEY_NULL = 0,

        KEY_APOSTROPHE = 39,
        KEY_COMMA = 44,
        KEY_MINUS = 45,
        KEY_PERIOD = 46,
        KEY_SLASH = 47,
        KEY_ZERO = 48,
        KEY_ONE = 49,
        KEY_TWO = 50,
        KEY_THREE = 51,
        KEY_FOUR = 52,
        KEY_FIVE = 53,
        KEY_SIX = 54,
        KEY_SEVEN = 55,
        KEY_EIGHT = 56,
        KEY_NINE = 57,
        KEY_SEMICOLON = 59,
        KEY_EQUAL = 61,
        KEY_A = 65,
        KEY_B = 66,
        KEY_C = 67,
        KEY_D = 68,
        KEY_E = 69,
        KEY_F = 70,
        KEY_G = 71,
        KEY_H = 72,
        KEY_I = 73,
        KEY_J = 74,
        KEY_K = 75,
        KEY_L = 76,
        KEY_M = 77,
        KEY_N = 78,
        KEY_O = 79,
        KEY_P = 80,
        KEY_Q = 81,
        KEY_R = 82,
        KEY_S = 83,
        KEY_T = 84,
        KEY_U = 85,
        KEY_V = 86,
        KEY_W = 87,
        KEY_X = 88,
        KEY_Y = 89,
        KEY_Z = 90,

        KEY_SPACE = 32,
        KEY_ESCAPE = 256,
        KEY_ENTER = 257,
        KEY_TAB = 258,
        KEY_BACKSPACE = 259,
        KEY_INSERT = 260,
        KEY_DELETE = 261,
        KEY_RIGHT = 262,
        KEY_LEFT = 263,
        KEY_DOWN = 264,
        KEY_UP = 265,
        KEY_PAGE_UP = 266,
        KEY_PAGE_DOWN = 267,
        KEY_HOME = 268,
        KEY_END = 269,
        KEY_CAPS_LOCK = 280,
        KEY_SCROLL_LOCK = 281,
        KEY_NUM_LOCK = 282,
        KEY_PRINT_SCREEN = 283,
        KEY_PAUSE = 284,
        KEY_F1 = 290,
        KEY_F2 = 291,
        KEY_F3 = 292,
        KEY_F4 = 293,
        KEY_F5 = 294,
        KEY_F6 = 295,
        KEY_F7 = 296,
        KEY_F8 = 297,
        KEY_F9 = 298,
        KEY_F10 = 299,
        KEY_F11 = 300,
        KEY_F12 = 301,
        KEY_LEFT_SHIFT = 340,
        KEY_LEFT_CONTROL = 341,
        KEY_LEFT_ALT = 342,
        KEY_LEFT_SUPER = 343,
        KEY_RIGHT_SHIFT = 344,
        KEY_RIGHT_CONTROL = 345,
        KEY_RIGHT_ALT = 346,
        KEY_RIGHT_SUPER = 347,
        KEY_KB_MENU = 348,
        KEY_LEFT_BRACKET = 91,
        KEY_BACKSLASH = 92,
        KEY_RIGHT_BRACKET = 93,
        KEY_GRAVE = 96,

        KEY_KP_0 = 320,
        KEY_KP_1 = 321,
        KEY_KP_2 = 322,
        KEY_KP_3 = 323,
        KEY_KP_4 = 324,
        KEY_KP_5 = 325,
        KEY_KP_6 = 326,
        KEY_KP_7 = 327,
        KEY_KP_8 = 328,
        KEY_KP_9 = 329,
        KEY_KP_DECIMAL = 330,
        KEY_KP_DIVIDE = 331,
        KEY_KP_MULTIPLY = 332,
        KEY_KP_SUBTRACT = 333,
        KEY_KP_ADD = 334,
        KEY_KP_ENTER = 335,
        KEY_KP_EQUAL = 336,

        KEY_BACK = 4,
        KEY_MENU = 82,
        KEY_VOLUME_UP = 24,
        KEY_VOLUME_DOWN = 25
    }

    public enum MouseButton
    {
        MOUSE_BUTTON_LEFT = 0,
        MOUSE_BUTTON_RIGHT = 1,
        MOUSE_BUTTON_MIDDLE = 2,
        MOUSE_BUTTON_SIDE = 3,
        MOUSE_BUTTON_EXTRA = 4,
        MOUSE_BUTTON_FORWARD = 5,
        MOUSE_BUTTON_BACK = 6,

        MOUSE_LEFT_BUTTON = MOUSE_BUTTON_LEFT,
        MOUSE_RIGHT_BUTTON = MOUSE_BUTTON_RIGHT,
        MOUSE_MIDDLE_BUTTON = MOUSE_BUTTON_MIDDLE,
    }

    public enum MouseCursor
    {
        MOUSE_CURSOR_DEFAULT = 0,
        MOUSE_CURSOR_ARROW = 1,
        MOUSE_CURSOR_IBEAM = 2,
        MOUSE_CURSOR_CROSSHAIR = 3,
        MOUSE_CURSOR_POINTING_HAND = 4,
        MOUSE_CURSOR_RESIZE_EW = 5,
        MOUSE_CURSOR_RESIZE_NS = 6,
        MOUSE_CURSOR_RESIZE_NWSE = 7,
        MOUSE_CURSOR_RESIZE_NESW = 8,
        MOUSE_CURSOR_RESIZE_ALL = 9,
        MOUSE_CURSOR_NOT_ALLOWED = 10
    }

    public enum GamepadAxis
    {
        GAMEPAD_AXIS_LEFT_X = 0,
        GAMEPAD_AXIS_LEFT_Y = 1,
        GAMEPAD_AXIS_RIGHT_X = 2,
        GAMEPAD_AXIS_RIGHT_Y = 3,
        GAMEPAD_AXIS_LEFT_TRIGGER = 4,
        GAMEPAD_AXIS_RIGHT_TRIGGER = 5
    }

    public enum GamepadButton
    {
        GAMEPAD_BUTTON_UNKNOWN = 0,
        GAMEPAD_BUTTON_LEFT_FACE_UP,
        GAMEPAD_BUTTON_LEFT_FACE_RIGHT,
        GAMEPAD_BUTTON_LEFT_FACE_DOWN,
        GAMEPAD_BUTTON_LEFT_FACE_LEFT,
        GAMEPAD_BUTTON_RIGHT_FACE_UP,
        GAMEPAD_BUTTON_RIGHT_FACE_RIGHT,
        GAMEPAD_BUTTON_RIGHT_FACE_DOWN,
        GAMEPAD_BUTTON_RIGHT_FACE_LEFT,
        GAMEPAD_BUTTON_LEFT_TRIGGER_1,
        GAMEPAD_BUTTON_LEFT_TRIGGER_2,
        GAMEPAD_BUTTON_RIGHT_TRIGGER_1,
        GAMEPAD_BUTTON_RIGHT_TRIGGER_2,
        GAMEPAD_BUTTON_MIDDLE_LEFT,
        GAMEPAD_BUTTON_MIDDLE,
        GAMEPAD_BUTTON_MIDDLE_RIGHT,
        GAMEPAD_BUTTON_LEFT_THUMB,
        GAMEPAD_BUTTON_RIGHT_THUMB
    }

    internal enum InputType
    {
        Keyboard,
        Mouse,
        Gamepad
    }

    internal struct InputEntry
    {
        public InputType Type;
        public Enum Input;
    }

    internal struct Mapping
    {
        public List<InputEntry> Inputs;
    }

    public class Input
    {
        private static Input _instance;

        private Dictionary<string, Mapping> _mappings;
        private bool _mouseCursorDisabled = false;

        private Input()
        {
            _mappings = new Dictionary<string, Mapping>();
        }

        public static Input Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Input();

                return _instance;
            }
        }

        public Vector2 MousePosition
        {
            get
            {
                return Game.Instance.VirtualMouse;
            }
        }

        public Vector2 MouseDelta
        {
            get
            {
                return Raylib.GetMouseDelta();
            }
        }

        public Vector2 MouseScrollDelta
        {
            get
            {
                return Raylib.GetMouseWheelMoveV();
            }
        }

        public MouseCursor MouseCursor
        {
            set
            {
                Raylib.SetMouseCursor((Raylib_cs.MouseCursor)value);
                Logger.Debug("Set mouse cursor to " + value);
            }
        }

        public bool MouseCursorHidden
        {
            get
            {
                return Raylib.IsCursorHidden();
            }
            set
            {
                if (value)
                {
                    Raylib.HideCursor();
                    Logger.Debug("Cursor hidden");
                }
                else
                {
                    Raylib.ShowCursor();
                    Logger.Debug("Cursor visible");
                }
            }
        }

        public bool MouseCursorDisabled
        {
            get
            {
                return _mouseCursorDisabled;
            }
            set
            {
                if (value)
                {
                    Raylib.DisableCursor();
                    Logger.Debug("Cursor disabled");
                }
                else
                {
                    Raylib.EnableCursor();
                    Logger.Debug("Cursor enabled");
                }

                _mouseCursorDisabled = value;
            }
        }

        public void CreateMapping(string name)
        {
            if (_mappings.ContainsKey(name))
                return;

            var mapping = new Mapping();
            mapping.Inputs = new List<InputEntry>();

            _mappings[name] = mapping;

            Logger.Debug("Created a new input mapping: " + name);
        }

        public void AddKey(string mapping, KeyboardKey key)
        {
            if (!_mappings.ContainsKey(mapping))
                return;

            var entry = new InputEntry();

            entry.Type = InputType.Keyboard;
            entry.Input = key;

            _mappings[mapping].Inputs.Add(entry);

            Logger.Debug("Added a new key to input mapping: " + mapping + " - " + key);
        }

        public void RemoveKey(string mapping, KeyboardKey key)
        {
            if (!_mappings.ContainsKey(mapping))
                return;

            foreach (var entry in _mappings[mapping].Inputs)
            {
                if ((KeyboardKey)entry.Input == key)
                {
                    _mappings.Remove(mapping);
                }
            }

            Logger.Debug("Removed a key from input mapping: " + mapping + " - " + key);
        }

        public void AddMouseButton(string mapping, MouseButton button)
        {
            if (!_mappings.ContainsKey(mapping))
                return;

            var entry = new InputEntry();

            entry.Type = InputType.Mouse;
            entry.Input = button;

            _mappings[mapping].Inputs.Add(entry);

            Logger.Debug("Added a new mouse button to input mapping: " + mapping + " - " + button);
        }

        public void RemoveMouseButton(string mapping, MouseButton button)
        {
            if (!_mappings.ContainsKey(mapping))
                return;

            foreach (var entry in _mappings[mapping].Inputs)
            {
                if ((MouseButton)entry.Input == button)
                {
                    _mappings.Remove(mapping);
                }
            }

            Logger.Debug("Removed a mouse button from input mapping: " + mapping + " - " + button);
        }

        public void AddGamepadButton(string mapping, GamepadButton button)
        {
            if (!_mappings.ContainsKey(mapping))
                return;

            var entry = new InputEntry();

            entry.Type = InputType.Gamepad;
            entry.Input = button;

            _mappings[mapping].Inputs.Add(entry);

            Logger.Debug("Added a new gamepad button to input mapping: " + mapping + " - " + button);
        }

        public void RemoveGamepadButton(string mapping, GamepadButton button)
        {
            if (!_mappings.ContainsKey(mapping))
                return;

            foreach (var entry in _mappings[mapping].Inputs)
            {
                if ((GamepadButton)entry.Input == button)
                {
                    _mappings.Remove(mapping);
                }
            }

            Logger.Debug("Removed a gamepad button from input mapping: " + mapping + " - " + button);
        }

        public bool GetButton(string button)
        {
            if (!_mappings.ContainsKey(button))
                return false;

            var mapping = _mappings[button];

            foreach (var entry in mapping.Inputs)
            {
                switch (entry.Type)
                {
                    case InputType.Keyboard:
                        if (Raylib.IsKeyDown((Raylib_cs.KeyboardKey)entry.Input))
                            return true;

                        break;

                    case InputType.Mouse:
                        if (Raylib.IsMouseButtonDown((Raylib_cs.MouseButton)entry.Input))
                            return true;

                        break;

                    case InputType.Gamepad:
                        if (Raylib.IsGamepadButtonDown(0, (Raylib_cs.GamepadButton)entry.Input))
                            return true;

                        break;
                }
            }

            return false;
        }

        public bool GetButtonDown(string button)
        {
            if (!_mappings.ContainsKey(button))
                return false;

            var mapping = _mappings[button];

            foreach (var entry in mapping.Inputs)
            {
                switch (entry.Type)
                {
                    case InputType.Keyboard:
                        if (Raylib.IsKeyPressed((Raylib_cs.KeyboardKey)entry.Input))
                            return true;

                        break;

                    case InputType.Mouse:
                        if (Raylib.IsMouseButtonPressed((Raylib_cs.MouseButton)entry.Input))
                            return true;

                        break;

                    case InputType.Gamepad:
                        if (Raylib.IsGamepadButtonPressed(0, (Raylib_cs.GamepadButton)entry.Input))
                            return true;

                        break;
                }
            }

            return false;
        }

        public bool GetButtonUp(string button)
        {
            if (!_mappings.ContainsKey(button))
                return false;

            var mapping = _mappings[button];

            foreach (var entry in mapping.Inputs)
            {
                switch (entry.Type)
                {
                    case InputType.Keyboard:
                        if (Raylib.IsKeyReleased((Raylib_cs.KeyboardKey)entry.Input))
                            return true;

                        break;

                    case InputType.Mouse:
                        if (Raylib.IsMouseButtonReleased((Raylib_cs.MouseButton)entry.Input))
                            return true;

                        break;

                    case InputType.Gamepad:
                        if (Raylib.IsGamepadButtonReleased(0, (Raylib_cs.GamepadButton)entry.Input))
                            return true;

                        break;
                }
            }

            return false;
        }

        public bool GetKey(KeyboardKey key)
        {
            return Raylib.IsKeyDown((Raylib_cs.KeyboardKey)key);
        }

        public bool GetKeyDown(KeyboardKey key)
        {
            return Raylib.IsKeyPressed((Raylib_cs.KeyboardKey)key);
        }

        public bool GetKeyUp(KeyboardKey key)
        {
            return Raylib.IsKeyReleased((Raylib_cs.KeyboardKey)key);
        }

        public bool GetMouseButton(MouseButton button)
        {
            return Raylib.IsMouseButtonDown((Raylib_cs.MouseButton)button);
        }

        public bool GetMouseButtonDown(MouseButton button)
        {
            return Raylib.IsMouseButtonPressed((Raylib_cs.MouseButton)button);
        }

        public bool GetMouseButtonUp(MouseButton button)
        {
            return Raylib.IsMouseButtonReleased((Raylib_cs.MouseButton)button);
        }

        public List<string> GetGamepadNames()
        {
            var list = new List<string>();

            for (var i = 0; i <= 4; i++)
            {
                if (Raylib.IsGamepadAvailable(i))
                {
                    var name = Raylib.GetMonitorName_(i);

                    list.Add(name);
                    Logger.Debug("Detected gamepad named: " + name);
                }
            }

            return list;
        }
    }
}
