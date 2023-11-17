using Snake.Contracts;

namespace Snake
{ 
    public class Program
    {
        private static IWriter writer = new Writer();

        private static Snake snake = new Snake(writer);       

        private static Game game = new Game(snake, writer);               

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