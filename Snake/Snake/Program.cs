using Snake.Contracts;
using Snake.Implementations;

namespace Snake
{ 
    public class Program
    {      
        private static IWriter writer = new Writer();

        private static ISnake snake = new Implementations.Snake(writer);

        private static IScore score = new Score(writer);

        private static IGameSpeed speed = new GameSpeed();

        private static IBoard board = new Board(writer);               

        public static void Main()
        {
            IGame game = new Game(snake, writer, score, speed, board);
            RunGame(game);            
        }

        private static void RunGame(IGame game)
        {
            game.Run();
        } 
    }
}