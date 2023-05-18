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
