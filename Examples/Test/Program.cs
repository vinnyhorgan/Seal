using Seal;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var game = new Game(new TestSettings()))
            {
                SceneManager.Instance.CurrentScene = new TestScene();

                var testManager = new GameObject("Test Manager");
                testManager.AddComponent<TestManager>();

                game.Run();
            }
        }
    }
}
