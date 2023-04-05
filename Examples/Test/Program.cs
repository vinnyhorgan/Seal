using Seal;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new Settings();

            settings.Title = "Test";
            settings.Resizable = true;

            using (var game = new Game(settings))
            {
                Scene scene = new Scene("Test Scene");
                SceneManager.Instance.CurrentScene = scene;

                var testManager = new GameObject("Test Manager");
                testManager.AddComponent<TestManager>();

                game.Run();
            }
        }
    }
}
