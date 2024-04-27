using Snake.Contracts;

namespace Snake
{ 
    public class Program
    {      
        private static IWriter writer = new Writer();

        private static ISnake snake = new Snake(writer);

        private static IScore score = new Score();

        private static IGameSpeed speed = new GameSpeed();

        private static IBoard board = new Board(writer);               

        public static void Main()
        {
            Game game = new Game(snake, writer, score, speed, board);
            RunGame(game);            
        }

        private static void RunGame(Game game)
        {
            game.Run();
        } 
    }
}