using System.Collections.Generic;
using System.Numerics;
using Seal.Components;

namespace Seal
{
    public abstract class Component
    {
        private bool _enabled;

        protected Component()
        {
            Id = -1;
            GameObject = null;
            Enabled = true;
        }

        public int Id { get; internal set; }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled != value)
                {
                    _enabled = value;

                    if (_enabled)
                    {
                        OnEnable();
                    }
                    else
                    {
                        OnDisable();
                    }
                }
            }
        }

        // Scripting API
        public GameObject GameObject { get; internal set; }
        public string Name { get { return GameObject.Name; } }
        public string Tag { get { return GameObject.Tag; } }
        public Transform Transform { get { return GameObject.Transform; } }
        public Scene Scene { get { return GameObject.Scene; } }
        public Game Game { get { return Game.Instance; } }
        public SceneManager SceneManager { get { return SceneManager.Instance; } }
        public AssetManager AssetManager { get { return AssetManager.Instance; } }
        public Input Input { get { return Input.Instance; } }

        public T GetComponent<T>() where T : Component
        {
            return GameObject.GetComponent<T>();
        }

        public List<T> GetComponents<T>() where T : Component
        {
            return GameObject.GetComponents<T>();
        }

        public void Destroy(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
        }

        public void Destroy(Component component)
        {
            GameObject.Destroy(component);
        }

        public void Instantiate(GameObject gameObject)
        {
            GameObject.Instantiate(gameObject);
        }

        public void Instantiate(GameObject gameObject, Vector2 position)
        {
            GameObject.Instantiate(gameObject, position);
        }

        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
        public virtual void OnGUI() { }
        public virtual void OnDestroy() { }
        public virtual void OnEnable() { }
        public virtual void OnDisable() { }
    }
}
