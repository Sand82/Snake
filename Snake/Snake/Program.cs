using System;
using System.Threading;

namespace Snake
{
    public class Player
    {
        public Player(int xPosition, int yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }

        public int xPosition { get; set; }

        public int yPosition { get; set; }

        public void PlayerStartPosition()
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.WriteLine('@');
        }

        public void ChangePlayerPosition(int xPosition, int yPosition, int xMove, int yMove)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.WriteLine(' ');

            if (xPosition == xMove)
            {
                yPosition = yMove;
            }
            else
            {
                xPosition = xMove;
            }

            Console.SetCursorPosition(xPosition, yPosition);
            Console.WriteLine('@');
        }
    }

    public class Food
    {
        public Food(int xPosition, int yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;  
        }

        public int xPosition { get; set; }

        public int yPosition { get; set; }
    }

    public class Program
    {
        private static char food = '#';

        private static List<Food> foods = new();        

        public static void Main(string[] args)
        {
            Player player = new Player(40, 15);

            DrawABox(0, 0, 80, 30, '|', '-', "test test");

            player.PlayerStartPosition();

            var direction = "Right";

            var isGameOver = false;

            while (isGameOver != true)
            {                  
               
                while (isGameOver != true)
                {

                    while (foods.Count < 3)
                    {
                        SetFood();
                    }

                    Thread.Sleep(100);

                    if (Console.KeyAvailable)
                    {
                        var info = Console.ReadKey(true);

                        var key = info.Key;

                        direction = GetPlayerDirection(direction, key);
                    }                    

                    var (xValue, yValue) = GetNewPosition(direction, player.xPosition, player.yPosition);

                    PrintInConsole(' ', player.xPosition, player.yPosition);

                    player.xPosition = xValue;
                    player.yPosition = yValue;

                    PrintInConsole('@', player.xPosition, player.yPosition);

                    if (player.xPosition == 0 || player.xPosition == 80 || player.yPosition == 0 || player.yPosition == 30)
                    {
                        isGameOver = true;
                    }

                    foreach (var food in foods)
                    {
                        if (player.xPosition == food.xPosition && player.yPosition == food.yPosition)
                        {
                            PrintInConsole('@', player.xPosition, player.yPosition);
                            foods.Remove(food);
                            break;
                        }
                    }
                }                
            }           
        }

        private static void PrintInConsole( char symbol, int xPosition, int yPosition)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(symbol);
        }
        
        private static (int, int) GetNewPosition(string direction, int xValue, int yValue)
        {                    
            Dictionary<string, int> directionValues = new Dictionary<string, int>()
            {            
                { "Up", - 1 },
                { "Down", 1 },
                { "Left", - 1 },
                { "Right",  1 },
            };

            if (direction == "Up" || direction == "Down")
            {
                yValue += directionValues[direction];
            }

            if (direction == "Left" || direction == "Right")
            {
                xValue += directionValues[direction];
            }

            return (xValue, yValue);
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
    }
}