namespace Snake
{
    public class Snake
    {
        public Snake()
        {
            if (this.Head == null)
            {
                var head = new SnakePart(40, 15);
                this.Head = head;
                this.Body.Add(head);
            }
        }

        public SnakePart Head { get; set; }

        public List<SnakePart> Body { get; set; } = new List<SnakePart>();        

        public void AddBodyElemen(int xPosition, int yPosition)
        {
            var bodyElement = new SnakePart(xPosition, yPosition);

            Body.Add(bodyElement);
        }

        public void MoveSnake(int xValue, int yValue)
        {
            var lastXPosition = 0;
            var lastYPosition = 0;

            for (int i = 0; i < Body.Count; i++)
            {
                var item = Body[i];

                if (item.xPosition == Head.xPosition && item.yPosition == Head.yPosition)
                {
                    lastXPosition = item.xPosition;
                    lastYPosition = item.yPosition;
                    item.xPosition += xValue;
                    item.yPosition += yValue;
                    PrintInConsole('@', item.xPosition, item.yPosition);
                    
                }
                else
                {
                    var tempX = item.xPosition;
                    var tempY = item.yPosition;

                    item.xPosition = lastXPosition;
                    lastXPosition  = tempX;

                    item.yPosition = lastYPosition;
                    lastYPosition = tempY;
                    PrintInConsole('@', item.xPosition, item.yPosition);
                }                
                PrintInConsole(' ', lastXPosition, lastYPosition);
                               
            }
        }

        private static void PrintInConsole(char symbol, int xPosition, int yPosition)
        {
            Console.SetCursorPosition(xPosition, yPosition);
            Console.Write(symbol);
        }
    }
}