using Raylib_cs;
using System.Numerics;

namespace Seal
{
    public class Camera
    {
        private Camera2D _camera;
        public Camera()
        {
            _camera = new Camera2D
            {
                target = new Vector2(0, 0),
                offset = new Vector2(0, 0),
                rotation = 0,
                zoom = 1
            };
        }

        public Vector2 Position
        {
            get { return _camera.target; }
            set { _camera.target = value; }
        }

        public float Zoom
        {
            get { return _camera.zoom; }
            set { _camera.zoom = value; }
        }

        public float Rotation
        {
            get { return _camera.rotation; }
            set { _camera.rotation = value; }
        }

        internal Camera2D Camera2D { get { return _camera; } }
    }
}
