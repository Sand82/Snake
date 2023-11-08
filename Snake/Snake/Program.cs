namespace Snake
{ 
    public class Program
    {
        private static Snake snake = new Snake();

        private static Game game = new Game(snake);               

        public static void Main()
        {
            RunTheGame();            
        }

        private static void RunTheGame()
        {
            game.Run();
        }        
    }
}