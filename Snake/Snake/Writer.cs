using Snake.Contracts;

namespace Snake
{
    public class Writer : IWriter
    { 
        public void PrintInConsole(char symbol, int xPosition, int yPosition)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(symbol);
        }

        public void SetCursorPosition(int left, int right)
        {
            Console.SetCursorPosition(left, right);
        }

        public void Write(char symbol)
        {
            Console.Write(symbol);
        }

        public void Write(string symbol)
        {
            Console.Write(symbol);
        }

        public bool KeyAvailable()
        {
            return Console.KeyAvailable;
        }

        public ConsoleKey Key()
        {
            var info = Console.ReadKey(true);

            return info.Key;            
        }
    }
}
