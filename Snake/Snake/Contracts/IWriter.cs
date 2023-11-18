namespace Snake.Contracts
{
    public interface IWriter
    {
        public void PrintInConsole(char symbol, int xPosition, int yPosition);        

        public void SetCursorPosition(int left, int right);

        public void Write(char symbol);

        public void Write(string symbol);

        public bool KeyAvailable();

        public void WriteInPosition(int xPosition, int yPosition, string message);

        public ConsoleKey Key();
    }
}
