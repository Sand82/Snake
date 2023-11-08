namespace Snake
{
    public class Game
    {
        private List<Food> foods = new();

        private string direction = "Right";

        private bool isGameOver = false;

        public Game(Snake snake )
        {
            this.Snake = snake;
        }

        public Snake Snake { get; set; }

        public void Run()
        {
            DrawABox(0, 0, 80, 30, '|', '-', "test test");

            PrintInConsole('@', Snake.Head.xPosition, Snake.Head.yPosition);
            PrintInConsole(' ', Snake.Head.xPosition, Snake.Head.yPosition);

            while (isGameOver != true)
            {
                while (isGameOver != true)
                {
                    while (foods.Count < 3)
                    {
                        SetFood();
                    }

                    Thread.Sleep(300);

                    if (Console.KeyAvailable)
                    {
                        var info = Console.ReadKey(true);

                        var key = info.Key;

                        direction = GetPlayerDirection(direction, key);
                    }

                    var (xValue, yValue) = GetNewPosition(direction);

                    var nextXPosition = Snake.Head.xPosition + xValue;
                    var nextYPosition = Snake.Head.yPosition + yValue;

                    if (Snake.Head.xPosition == 0 || Snake.Head.xPosition == 80 || Snake.Head.yPosition == 0 || Snake.Head.yPosition == 30)
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
                { "Up", "-1, 0" },
                { "Down", "1, 0" },
                { "Left", "0, -1" },
                { "Right", "0, 1" },
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

            Console.SetCursorPosition(xValue, yValue);
            Console.WriteLine(food.foodSymbol);

            foods.Add(food);
        }

        private static void PrintInConsole(char symbol, int xPosition, int yPosition)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(symbol);
        }

        private void DrawABox(int x, int y, int width, int height, char EdgeX, char EdgeY, string Message)
        {
            int LastIndex = 0;
            Console.SetCursorPosition(x, y);
            for (int h_i = 0; h_i <= height; h_i++)
            {
                if (LastIndex != -1)
                {
                    int seaindex = (LastIndex + (width - 1));
                    if (seaindex >= Message.Length - 1)
                        seaindex = Message.Length - 1;
                    int newIndex = Message.LastIndexOf(' ', seaindex);
                    if (newIndex == -1)
                        newIndex = Message.Length - 1;
                    string substr = Message.Substring(LastIndex, newIndex - LastIndex);
                    LastIndex = newIndex;
                    Console.SetCursorPosition(x + 1, y + h_i);
                    Console.Write(substr);
                }

                var maxYRol = width;

                for (int w_i = 0; w_i <= width; w_i++)
                {
                    if (h_i % height == 0 || w_i % width == 0)
                    {
                        Console.SetCursorPosition(x + w_i, y + h_i);

                        if (w_i == 0 || w_i == maxYRol)
                        {
                            Console.Write(EdgeX);
                        }
                        else
                        {
                            Console.Write(EdgeY);
                        }

                    }
                }
            }
        }
    }
}
