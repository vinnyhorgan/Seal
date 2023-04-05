using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Seal.Components;

namespace Seal
{
    public class GameObject
    {
        public string Name;
        public string Tag;

        private List<Component> _components;
        private List<Component> _componentsToAdd;
        private bool _active;

        public GameObject(string name = "")
        {
            // TODO make loading async

            if (name == "")
            {
                Name = GetType().Name;
            }
            else
            {
                Name = name;
            }

            Tag = "Untagged";

            _components = new List<Component>();
            _componentsToAdd = new List<Component>();
            _active = true;

            Scene = SceneManager.Instance.CurrentScene;

            if (Scene != null)
            {
                Scene.GameObjects.Add(this);
                Id = Scene.NextId++;
                Transform = new Transform();

                Logger.Debug($"Created GameObject '{Name}' with id {Id}");
            }
            else
            {
                Logger.Error("No scene loaded!");

                Id = -1;
                Transform = new Transform();

                Game.Instance.Quit();
            }
        }

        public GameObject(string name = "", params Type[] components) : this(name)
        {
            foreach (var component in components)
            {
                var method = typeof(GameObject).GetMethod("AddComponent");

                if (method != null)
                {
                    var generic = method.MakeGenericMethod(component);
                    generic.Invoke(this, null);
                }
            }
        }

        public int Id { get; internal set; }
        public Scene Scene { get; internal set; }
        public Transform Transform { get; internal set; }

        public bool Active
        {
            get { return _active; }
            set
            {
                if (_active != value)
                {
                    foreach (var component in _components)
                    {
                        component.Enabled = _active;
                    }

                    _active = value;

                    Logger.Debug($"GameObject '{Name}' with id {Id} is now {(value ? "active" : "inactive")}");
                }
            }
        }

        public static void Update()
        {
            foreach (var scene in SceneManager.Instance.Scenes)
            {
                foreach (var gameObject in scene.GameObjects.ToList())
                {
                    foreach (var component in gameObject._componentsToAdd.ToList())
                    {
                        if (component.Enabled)
                        {
                            gameObject._components.Add(component);

                            component.OnEnable();
                            component.OnStart();
                        }

                        gameObject._componentsToAdd.Remove(component);
                    }
                }
            }
        }

        public static GameObject Find(string name)
        {
            foreach (var scene in SceneManager.Instance.Scenes)
            {
                foreach (var gameObject in scene.GameObjects)
                {
                    if (gameObject.Name == name && gameObject.Active)
                    {
                        return gameObject;
                    }
                }
            }

            return null;
        }

        public static GameObject FindWithTag(string tag)
        {
            foreach (var scene in SceneManager.Instance.Scenes)
            {
                foreach (var gameObject in scene.GameObjects)
                {
                    if (gameObject.Tag == tag && gameObject.Active)
                    {
                        return gameObject;
                    }
                }
            }

            return null;
        }

        public static List<GameObject> FindGameObjectsWithTag(string tag)
        {
            var list = new List<GameObject>();

            foreach (var scene in SceneManager.Instance.Scenes)
            {
                foreach (var gameObject in scene.GameObjects)
                {
                    if (gameObject.Tag == tag && gameObject.Active)
                    {
                        list.Add(gameObject);
                    }
                }
            }

            return list;
        }

        public static void Destroy(GameObject gameObject)
        {
            foreach (var component in gameObject._components)
            {
                component.OnDisable();
                component.OnDestroy();

                gameObject._components.Remove(component);
            }

            foreach (var scene in SceneManager.Instance.Scenes)
            {
                scene.GameObjects.Remove(gameObject);
            }

            Logger.Debug($"Destroyed GameObject '{gameObject.Name}' with id {gameObject.Id}");
        }

        public static void Destroy(Component component)
        {
            component.OnDisable();
            component.OnDestroy();
            component.GameObject._components.Remove(component);

            Logger.Debug($"Destroyed Component '{component.GetType().Name}' from GameObject '{component.GameObject.Name}' with id {component.GameObject.Id}");
        }

        public static GameObject Instantiate(GameObject gameObject)
        {
            var clone = new GameObject(gameObject.Name);

            clone.Tag = gameObject.Tag;

            clone.Transform.Position = gameObject.Transform.Position;

            // TODO clone components properly...

            foreach (var component in gameObject._components)
            {
                var method = typeof(GameObject).GetMethod("AddComponent");

                if (method != null)
                {
                    var generic = method.MakeGenericMethod(component.GetType());
                    generic.Invoke(clone, null);
                }
            }

            return clone;
        }

        public static GameObject Instantiate(GameObject gameObject, Vector2 position)
        {
            var clone = Instantiate(gameObject);
            clone.Transform.Position = position;
            return clone;
        }

        public T AddComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component is T)
                {
                    Logger.Error($"GameObject '{Name}' with id {Id} already has a component of type '{typeof(T).Name}'");
                    return null;
                }
            }

            var newComponent = Activator.CreateInstance<T>();

            newComponent.Id = _components.Count;
            newComponent.GameObject = this;

            _componentsToAdd.Add(newComponent);

            return newComponent;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (var component in _components)
            {
                if (component is T)
                {
                    return (T)component;
                }
            }

            return null;
        }

        public List<T> GetComponents<T>() where T : Component
        {
            var list = new List<T>();

            foreach (var component in _components)
            {
                if (component is T)
                {
                    list.Add((T)component);
                }
            }

            return list;
        }

        internal void OnUpdate()
        {
            foreach (var component in _components)
            {
                if (component.Enabled)
                {
                    component.OnUpdate();
                }
            }
        }

        internal void OnGUI()
        {
            foreach (var component in _components)
            {
                if (component.Enabled)
                {
                    component.OnGUI();
                }
            }
        }
    }
}
