using System.Collections.Generic;

namespace Seal
{
    public class Scene
    {
        public string Name;

        internal int NextId;

        private List<GameObject> _gameObjects;

        public Scene(string name = "")
        {
            if (name == "")
            {
                Name = GetType().Name;
            }
            else
            {
                Name = name;
            }

            NextId = 0;

            _gameObjects = new List<GameObject>();

            Camera = new Camera();

            Logger.Debug("Created new Scene: " + Name);
        }

        public Camera Camera { get; private set; }

        internal List<GameObject> GameObjects { get { return _gameObjects; } }

        internal void OnUpdate()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.OnUpdate();
            }
        }

        internal void OnGUI()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.OnGUI();
            }
        }
    }
}
