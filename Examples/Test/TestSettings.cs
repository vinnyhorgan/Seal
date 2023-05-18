using Seal;

namespace Test
{
    class TestSettings : Settings
    {
        public TestSettings()
        {
            Title = "Test";
            Resizable = true;

            Input.CreateMapping("Move Left");

            Input.AddKey("Move Left", KeyboardKey.KEY_A);
            Input.AddGamepadButton("Move Left", GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT);

            Input.CreateMapping("Move Right");

            Input.AddKey("Move Right", KeyboardKey.KEY_D);
            Input.AddGamepadButton("Move Right", GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT);
        }
    }
}
