using System;
using System.Threading;

namespace Snake
{  

    public class Program
    {
        private static char food = '#';

        private static List<Food> foods = new();        

        public static void Main(string[] args)
        {
            Snake snake = new Snake();

            DrawABox(0, 0, 80, 30, '|', '-', "test test");

            PrintInConsole('@', snake.Head.xPosition, snake.Head.yPosition);

            var direction = "Right";

            var isGameOver = false;

            var testFood = new Food(41, 15);

            Console.SetCursorPosition(testFood.xPosition, testFood.yPosition);
            Console.WriteLine(food);

            foods.Add(testFood);

            while (isGameOver != true)
            { 
                while (isGameOver != true)
                {
                    while (foods.Count < 3)
                    {                        
                        SetFood();
                    }

                    Thread.Sleep(500);

                    if (Console.KeyAvailable)
                    {
                        var info = Console.ReadKey(true);

                        var key = info.Key;

                        direction = GetPlayerDirection(direction, key);
                    }                    

                    var (xValue, yValue) = GetNewPosition(direction);

                    var nextXPosition = snake.Head.xPosition + xValue;
                    var nextYPosition = snake.Head.yPosition + yValue;

                    if (snake.Head.xPosition == 0 || snake.Head.xPosition == 80 || snake.Head.yPosition == 0 || snake.Head.yPosition == 30)
                    {
                        isGameOver = true;
                    }

                    foreach (var food in foods)
                    {
                        if (nextXPosition == food.xPosition && nextYPosition == food.yPosition)
                        {
                            foods.Remove(food);
                            snake.AddBodyElemen(food.xPosition, food.yPosition);
                            break;
                        }
                    }

                    snake.MoveSnake(xValue, yValue);                                
                }                
            }           
        }
        
        private static (int, int) GetNewPosition(string direction)
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
        
        private static string GetPlayerDirection(string direction, ConsoleKey key)
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

        private static void SetFood()
        {
            var x = new Random();
            int xValue = x.Next(1, 79);

            var y = new Random();
            int yValue = y.Next(1, 29);

            Console.SetCursorPosition(xValue, yValue);
            Console.WriteLine(food);

            var foodPositions = new Food(xValue, yValue);

            foods.Add(foodPositions);
        }

        private static void DrawABox(int x, int y, int width, int height, char EdgeX, char EdgeY, string Message)
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

        private static void PrintInConsole(char symbol, int xPosition, int yPosition)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(symbol);
        }
    }
}