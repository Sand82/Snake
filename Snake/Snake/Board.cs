
using Snake.Contracts;
using System.Text.RegularExpressions;

namespace Snake
{
    public class Board
    { 
        private char edgeX = '|';

        private char edgeY = '-';

        private IWriter writer;

        public Board(IWriter writer)
        {
            this.writer = writer;
        }

        public int BoardStartPossitionX { get; private set; } = 0;

        public int BoardStartPossitionY { get;private set; } = 0;

        public int BoardWidth { get; private set; } = 80;

        public int BoardHeight { get; set; } = 30;

        public void DrawBorders(string message)
        {
            int lastIndex = 0;
            writer.SetCursorPosition(BoardStartPossitionX, BoardStartPossitionY);
            for (int h_i = 0; h_i <= BoardHeight; h_i++)
            {
                if (lastIndex != -1)
                {
                    int seaindex = (lastIndex + (BoardWidth - 1));
                    if (seaindex >= message.Length - 1)
                        seaindex = message.Length - 1;
                    int newIndex = message.LastIndexOf(' ', seaindex);
                    if (newIndex == -1)
                        newIndex = message.Length - 1;
                    string substr = message.Substring(lastIndex, newIndex - lastIndex);
                    lastIndex = newIndex;
                    writer.SetCursorPosition(BoardStartPossitionX + 1, BoardStartPossitionY + h_i);
                    writer.Write(substr);
                }

                var maxYRol = BoardWidth;

                for (int w_i = 0; w_i <= BoardWidth; w_i++)
                {
                    if (h_i % BoardHeight == 0 || w_i % BoardWidth == 0)
                    {
                        writer.SetCursorPosition(BoardStartPossitionX + w_i, BoardStartPossitionY + h_i);

                        if (w_i == 0 || w_i == maxYRol)
                        {
                            writer.Write(edgeX);
                        }
                        else
                        {
                            writer.Write(edgeY);
                        }

                    }
                }
            }
        }
    }
}
