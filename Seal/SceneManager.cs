using System.Collections.Generic;

namespace Seal
{
    public class SceneManager
    {
        private static SceneManager _instance;

        private Stack<Scene> _scenes;

        private SceneManager()
        {
            _scenes = new Stack<Scene>();
        }

        public static SceneManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SceneManager();

                return _instance;
            }
        }

        public Scene CurrentScene
        {
            get
            {
                if (_scenes.Count > 0)
                    return _scenes.Peek();

                return null;
            }
            set
            {
                if (_scenes.Count > 0)
                    _scenes.Pop();

                _scenes.Push(value);

                Logger.Debug("Current scene set to " + value.Name);
            }
        }

        internal Stack<Scene> Scenes
        {
            get { return _scenes; }
        }

        public void PushScene(Scene scene)
        {
            _scenes.Push(scene);

            Logger.Debug("Pushed scene " + scene.Name);
        }

        public void PopScene()
        {
            _scenes.Pop();

            Logger.Debug("Popped scene " + CurrentScene.Name);
        }

        public Scene GetScene(string name)
        {
            foreach (var scene in _scenes)
            {
                if (scene.Name == name)
                    return scene;
            }

            return null;
        }

        // TODO create method to load serialized scenes from file
    }
}
