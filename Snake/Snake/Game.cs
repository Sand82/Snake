using Snake.Contracts;

namespace Snake
{  
    public class Game
    {
        private const string right = "Right";
        private const string left = "Left";
        private const string up = "up";
        private const string down = "Down";

        private List<Food> foods = new();

        private string direction = right;

        private bool isGameOver = false;

        private IWriter writer;

        private int boardStartPossitionX = 0;

        private int boardStartPossitionY = 0;

        private int boardWidth = 80;

        private int boardHeight = 30;

        private char edgeX = '|';

        private char edgeY = '-';

        public Game(Snake snake, IWriter writer )
        {
            this.Snake = snake;
            this.writer = writer;
        }

        public Snake Snake { get; set; }

        public void Run()
        {
            DrawABox("test test");

            writer.PrintInConsole('@', Snake.Head.xPosition, Snake.Head.yPosition);
            writer.PrintInConsole(' ', Snake.Head.xPosition, Snake.Head.yPosition);

            while (isGameOver != true)
            {
                while (isGameOver != true)
                {
                    while (foods.Count < 3)
                    {
                        SetFood();
                    }

                    Thread.Sleep(100);

                    if (writer.KeyAvailable())
                    {
                        var key = writer.Key();

                        direction = GetPlayerDirection(direction, key);
                    }

                    var (xValue, yValue) = GetNewPosition(direction);

                    var nextXPosition = Snake.Head.xPosition + xValue;
                    var nextYPosition = Snake.Head.yPosition + yValue;

                    if (nextXPosition == boardStartPossitionX ||
                        nextXPosition == boardWidth ||
                        nextYPosition == boardStartPossitionY ||
                        nextYPosition == boardHeight)
                    {
                        isGameOver = true;
                    }

                    foreach (var food in foods)
                    {
                        if (nextXPosition == food.xPosition && nextYPosition == food.yPosition)
                        {
                            foods.Remove(food);
                            Snake.AddBodyElemen(food.xPosition, food.yPosition);
                            break;
                        }
                    }

                    foreach (var snakePart in Snake.Body.Skip(1))
                    {
                        if (Snake.Head.xPosition == snakePart.xPosition && Snake.Head.yPosition == snakePart.yPosition)
                        {
                            isGameOver = true;
                            break;
                        }
                    }

                    Snake.MoveSnake(xValue, yValue);
                }
            }
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

        private string GetPlayerDirection(string direction, ConsoleKey key)
        {
            if (key == ConsoleKey.UpArrow)
            {
                direction = "Up";
            }
            else if (key == ConsoleKey.DownArrow)
            {
                direction = "Down";
            }
            else if (key == ConsoleKey.LeftArrow)
            {
                direction = "Left";
            }
            else if (key == ConsoleKey.RightArrow)
            {
                direction = "Right";
            };

            return direction;
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

        private void DrawABox(string message)
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
