using Snake.Contracts;

namespace Snake.Implementations
{
    public class Board : IBoard
    {
        private char edgeX = '|';

        private char edgeY = '-';

        private IWriter writer;

        private int boardStartPositionX = 0;

        private int boardStartPositionY = 0;

        private int boardWidth = 80;

        private int boardHeight = 30;

        public Board(IWriter writer)
        {
            this.writer = writer;
        }

        public void DrawBorders(string message)
        {
            int lastIndex = 0;
            writer.SetCursorPosition(boardStartPositionX, boardStartPositionY);
            for (int h_i = 0; h_i <= boardHeight; h_i++)
            {
                if (lastIndex != -1)
                {
                    int seaindex = lastIndex + (boardWidth - 1);
                    if (seaindex >= message.Length - 1)
                        seaindex = message.Length - 1;
                    int newIndex = message.LastIndexOf(' ', seaindex);
                    if (newIndex == -1)
                        newIndex = message.Length - 1;
                    string substr = message.Substring(lastIndex, newIndex - lastIndex);
                    lastIndex = newIndex;
                    writer.SetCursorPosition(boardStartPositionX + 1, boardStartPositionY + h_i);
                    writer.Write(substr);
                }

                var maxYRol = boardWidth;

                for (int w_i = 0; w_i <= boardWidth; w_i++)
                {
                    if (h_i % boardHeight == 0 || w_i % boardWidth == 0)
                    {
                        writer.SetCursorPosition(boardStartPositionX + w_i, boardStartPositionY + h_i);

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

        public bool ValidateNextStep(int nextXPosition, int nextYPosition)
        {
            return nextXPosition == boardStartPositionX ||
                        nextXPosition == boardWidth ||
                        nextYPosition == boardStartPositionY ||
                        nextYPosition == boardHeight;
        }
    }
}
