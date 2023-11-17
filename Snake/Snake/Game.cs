using Snake.Contracts;

namespace Snake
{  
    public class Game
    {
        private const string right = "Right";
        private const string left = "Left";
        private const string up = "Up";
        private const string down = "Down";

        private int boardStartPossitionX = 0;
        private int boardStartPossitionY = 0;
        private int boardWidth = 80;
        private int boardHeight = 30;
        private char edgeX = '|';
        private char edgeY = '-';

        private List<Food> foods = new();

        private string direction = right;

        private bool isGameOver = false;

        private IWriter writer;        

        public Game(Snake snake, IWriter writer )
        {
            this.Snake = snake;
            this.writer = writer;
        }

        public Snake Snake { get; set; }

        public void Run()
        {
            DrawBorders("test test");

            writer.PrintInConsole('@', Snake.Head.xPosition, Snake.Head.yPosition);
            writer.PrintInConsole(' ', Snake.Head.xPosition, Snake.Head.yPosition);            

            while (isGameOver != true)
            {
                GamePipeLine();
            }

            writer.SetCursorPosition(0,32);

            writer.Write("Game Over!");
        }

        private void GamePipeLine()
        {
            var oldDirection = string.Empty;

            while (isGameOver != true)
            {
                oldDirection = direction;

                while (foods.Count < 3)
                {
                    SetFood();
                }

                Thread.Sleep(200);

                if (writer.KeyAvailable())
                {
                    var key = writer.Key();

                    GetSnakeDirection(key);

                    ValidateDirection(key, oldDirection);
                }

                var (xValue, yValue) = GetNewPosition(direction);

                var nextXPosition = GetNextStep(Snake.Head.xPosition, xValue);
                var nextYPosition = GetNextStep(Snake.Head.yPosition, yValue);

                isGameOver = ValidateNextStep(nextXPosition, nextYPosition);

                CheckForEatenFood(nextXPosition, nextYPosition);

                CheckSnakeForSelfBite(nextXPosition, nextYPosition);

                Snake.MoveSnake(xValue, yValue);
            }
        }

        private void ValidateDirection(ConsoleKey key , string oldDirection)
        {
            if (oldDirection == left && key == ConsoleKey.RightArrow)
            {
                direction = left;                
            }

            if (oldDirection == right && key == ConsoleKey.LeftArrow)
            {
                direction = right;
            }

            if (oldDirection == up && key == ConsoleKey.DownArrow)
            {
                direction = up;
            }

            if (oldDirection == down && key == ConsoleKey.UpArrow)
            {
                direction = down;
            }
        }

        private void CheckSnakeForSelfBite(int nextXPosition, int nextYPosition)
        {
            foreach (var snakePart in Snake.Body.Skip(1))
            {
                if (Snake.Head.xPosition == snakePart.xPosition && Snake.Head.yPosition == snakePart.yPosition)
                {
                    isGameOver = true;
                    break;
                }
            }
        }

        private void CheckForEatenFood(int nextXPosition, int nextYPosition)
        {
            foreach (var food in foods)
            {
                if (nextXPosition == food.xPosition && nextYPosition == food.yPosition)
                {
                    foods.Remove(food);
                    Snake.AddBodyElemen(food.xPosition, food.yPosition);
                    break;
                }
            }
        }

        private int GetNextStep(int headPosition, int step)
        {
            return step + headPosition;
        }

        private bool ValidateNextStep(int nextXPosition, int nextYPosition)
        {
            return nextXPosition == boardStartPossitionX ||
                        nextXPosition == boardWidth ||
                        nextYPosition == boardStartPossitionY ||
                        nextYPosition == boardHeight;
        }

        private (int, int) GetNewPosition(string direction)
        {
            Dictionary<string, string> directionValues = new Dictionary<string, string>()
            {
                { up, "-1, 0" },
                { down, "1, 0" },
                { left, "0, -1" },
                { right, "0, 1" },
            };

            var tokens = directionValues[direction]
                .Split(", ")
                .Select(int.Parse)
                .ToArray();

            return (tokens[1], tokens[0]);
        }

        private void GetSnakeDirection( ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow )
            {
                direction = up;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                direction = down;
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                direction = left;
            }
            else if (key == ConsoleKey.RightArrow)
            {
                direction = right;
            };            
        }

        private void SetFood()
        {
            var x = new Random();
            int xValue = x.Next(1, 79);

            var y = new Random();
            int yValue = y.Next(1, 29);


            var food = new Food(xValue, yValue);

            writer.PrintInConsole(food.foodSymbol, xValue, yValue);

            foods.Add(food);
        }        

        private void DrawBorders(string message)
        {
            int lastIndex = 0;
            writer.SetCursorPosition(boardStartPossitionX, boardStartPossitionY);
            for (int h_i = 0; h_i <= boardHeight; h_i++)
            {
                if (lastIndex != -1)
                {
                    int seaindex = (lastIndex + (boardWidth - 1));
                    if (seaindex >= message.Length - 1)
                        seaindex = message.Length - 1;
                    int newIndex = message.LastIndexOf(' ', seaindex);
                    if (newIndex == -1)
                        newIndex = message.Length - 1;
                    string substr = message.Substring(lastIndex, newIndex - lastIndex);
                    lastIndex = newIndex;
                    writer.SetCursorPosition(boardStartPossitionX + 1, boardStartPossitionY + h_i);
                    writer.Write(substr);
                }

                var maxYRol = boardWidth;

                for (int w_i = 0; w_i <= boardWidth; w_i++)
                {
                    if (h_i % boardHeight == 0 || w_i % boardWidth == 0)
                    {
                        writer.SetCursorPosition(boardStartPossitionX + w_i, boardStartPossitionY + h_i);

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
