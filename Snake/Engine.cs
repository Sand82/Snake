using Snake.Contracts;
using Snake.IOC.Attributes;

namespace Snake
{
    public class Engine
    {
        private IGame game;

        [Inject]
        public Engine(IGame game)
        {
            this.game = game;
        }

        public void StartGame()
        {
            game.Run();
        }
    }
}
