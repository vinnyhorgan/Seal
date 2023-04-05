using Seal;
using Seal.Components;
using System.Numerics;

namespace Test
{
    class TestManager : Component
    {
        private GameObject player;

        public override void OnStart()
        {
            Input.CreateMapping("Move Left");

            Input.AddKey("Move Left", KeyboardKey.KEY_A);
            Input.AddGamepadButton("Move Left", GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT);

            Input.CreateMapping("Move Right");

            Input.AddKey("Move Right", KeyboardKey.KEY_D);
            Input.AddGamepadButton("Move Right", GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT);

            player = new GameObject("Player");
            player.Transform.Position = new Vector2(100, 100);
            var spriteRenderer = player.AddComponent<SpriteRenderer>();
            spriteRenderer.Sprite = AssetManager.GetTexture("tile_0131");
        }

        public override void OnUpdate()
        {
            if (Input.GetButton("Move Left"))
            {
                player.Transform.Position -= new Vector2(1, 0);
            }

            if (Input.GetButton("Move Right"))
            {
                player.Transform.Position += new Vector2(1, 0);
            }
        }
    }
}
