using System.Collections.Generic;
using System.Numerics;

namespace Seal.Components
{
    public class Transform : Component
    {
        // TODO test thoroughly

        private List<Transform> _children;
        private Transform _parent;
        private Vector2 _position;
        private Vector2 _scale;
        private float _rotation;

        public Transform()
        {
            _children = new List<Transform>();
            _position = Vector2.Zero;
            _scale = Vector2.One;
            _rotation = 0.0f;

            Parent = null;
        }

        public int ChildCount { get { return _children.Count; } }

        public Transform Parent
        {
            get { return _parent; }
            set
            {
                if (_parent != value)
                {
                    if (_parent != null)
                    {
                        _parent._children.Remove(this);
                    }

                    _parent = value;

                    if (_parent != null)
                    {
                        _parent._children.Add(this);
                    }
                }
            }
        }

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                var translation = value - _position;
                _position = value;

                foreach (var child in _children)
                {
                    child.Position += translation;
                }
            }
        }

        public Vector2 LocalPosition
        {
            get
            {
                if (Parent == null)
                    return _position;

                return Position - Parent.LocalPosition;
            }
        }

        public Vector2 Scale
        {
            get { return _scale; }
            set
            {
                var scale = value / _scale;
                _scale = value;

                foreach (var child in _children)
                {
                    child.Scale *= scale;
                }
            }
        }

        public float Rotation
        {
            get { return _rotation; }
            set
            {
                var rotation = value - _rotation;
                _rotation = value;

                foreach (var child in _children)
                {
                    child.Rotation += rotation;
                }
            }
        }

        public Transform Root
        {
            get
            {
                var root = this;

                while (root.Parent != null)
                {
                    root = root.Parent;
                }

                return root;
            }
        }

        public Transform Find(string name)
        {
            if (GameObject.Name == name)
            {
                return this;
            }

            foreach (var child in _children)
            {
                if (child.GameObject.Name == name)
                {
                    return child;
                }
            }

            return null;
        }

        public Transform GetChild(int index)
        {
            if (index < _children.Count)
            {
                return _children[index];
            }

            return null;
        }

        public void DetachChildren()
        {
            foreach (var child in _children)
            {
                child.Parent = null;
            }

            _children.Clear();
        }
    }
}
