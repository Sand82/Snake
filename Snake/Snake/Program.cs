using Snake.Contracts;

namespace Snake
{ 
    public class Program
    {
        private static IWriter writer = new Writer();

        private static Snake snake = new Snake(writer);

        private static Score score = new Score();

        private static GameSpeed speed = new GameSpeed();

        private static Game game = new Game(snake, writer, score, speed);        

        public static void Main()
        {
            RunGame();            
        }

        private static void RunGame()
        {
            game.Run();
        }        
    }
}