using Raylib_cs;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace Seal
{
    public class Texture
    {
        internal Texture2D Texture2D;
    }

    internal struct TextureToLoad
    {
        public string Name;
        public string Path;
    }

    public class AssetManager
    {
        private static AssetManager _instance;

        private Dictionary<string, Texture> _textures;
        private List<TextureToLoad> _texturesToLoad;

        private AssetManager()
        {
            _textures = new Dictionary<string, Texture>();
            _texturesToLoad = new List<TextureToLoad>();
        }

        public static AssetManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AssetManager();

                return _instance;
            }
        }

        public bool Loading
        {
            get { return _texturesToLoad.Count > 0; }
        }

        public void LoadTexture(string name, string path)
        {
            if (_textures.ContainsKey(name))
                return;

            foreach (var textureToLoad in _texturesToLoad)
            {
                if (textureToLoad.Name == name)
                    return;
            }

            _texturesToLoad.Add(new TextureToLoad { Name = name, Path = path });
        }

        public Texture GetTexture(string name)
        {
            foreach (var textureToLoad in _texturesToLoad)
            {
                if (textureToLoad.Name == name)
                {
                    var texture = Raylib.LoadTexture(textureToLoad.Path);
                    _texturesToLoad.Remove(textureToLoad);

                    var loadedTexture = new Texture { Texture2D = texture };
                    _textures[textureToLoad.Name] = loadedTexture;

                    return loadedTexture;
                }
            }

            if (!_textures.ContainsKey(name))
                return null;

            return _textures[name];
        }

        public void ClearAssets()
        {
            foreach (var texture in _textures.Values)
                Raylib.UnloadTexture(texture.Texture2D);

            _textures.Clear();

            Logger.Debug("Cleared Assets");
        }

        internal void Init()
        {
            if (Directory.Exists("Assets"))
            {
                foreach (var file in Directory.GetFiles("Assets", "*.png", SearchOption.AllDirectories))
                {
                    var filename = Path.GetFileNameWithoutExtension(file);
                    LoadTexture(filename, file);
                }
            }

            Logger.Debug("Initialized AssetManager");
        }

        internal void Update()
        {
            // TODO make this a separate job system

            if (_texturesToLoad.Count > 0)
            {
                var textureToLoad = _texturesToLoad.Last();
                var texture = Raylib.LoadTexture(textureToLoad.Path);
                _texturesToLoad.Remove(textureToLoad);
                _textures[textureToLoad.Name] = new Texture { Texture2D = texture };

                Logger.Debug("Loaded Texture: " + textureToLoad.Name);
            }
        }
    }
}
