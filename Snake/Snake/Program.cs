using System;

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

            while (true)
            {                

                DrawABox(0, 0, 80, 30, '|', '-', "test test");

                while (foods.Count < 3)
                {
                    SetFood();
                }

                player.PlayerStartPosition();

                //ConsoleKey.                

               Console.ReadKey();
            }           
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