using Raylib_cs;
using System.Numerics;

namespace Seal.Components
{
    public class SpriteRenderer : Component
    {
        public Texture Sprite;
        public Color Color;
        public bool FlipX;
        public bool FlipY;

        public SpriteRenderer()
        {
            Sprite = null;
            Color = new Color { R = 255, G = 255, B = 255, A = 255 };
            FlipX = false;
            FlipY = false;
        }

        public override void OnUpdate()
        {
            /* if (Sprite != null)
            {
                var position = Transform.Position;
                var scale = Transform.Scale;
                var rotation = Transform.Rotation;

                Raylib.DrawTextureEx(Sprite.Texture2D, position, rotation, scale, new Raylib_cs.Color(Color.R, Color.G, Color.B, Color.A));
            } */

            // draw sprite accounting for flip

            if (Sprite != null)
            {
                var position = Transform.Position;
                var scale = Transform.Scale;
                var rotation = Transform.Rotation;
                var sourceRectangle = new Rectangle(0, 0, Sprite.Texture2D.width, Sprite.Texture2D.height);
                var origin = new Vector2(Sprite.Texture2D.width / 2, Sprite.Texture2D.height / 2);
                var destinationRectangle = new Rectangle(position.X, position.Y, Sprite.Texture2D.width * scale.X, Sprite.Texture2D.height * scale.Y);

                Raylib.DrawTexturePro(Sprite.Texture2D, sourceRectangle, destinationRectangle, origin, rotation, new Raylib_cs.Color(Color.R, Color.G, Color.B, Color.A));
            }
        }
    }
}
