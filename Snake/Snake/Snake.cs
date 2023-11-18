using Snake.Contracts;

namespace Snake
{
    public class Snake
    {
        private IWriter writer;

        public Snake(IWriter writer)
        {
            this.writer = writer;

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
                    writer.PrintInConsole('@', item.xPosition, item.yPosition);

                    if (Body.Count <= 2)
                    {
                        writer.PrintInConsole(' ', lastXPosition, lastYPosition);
                    }
                }
                else
                {
                    var tempX = item.xPosition;
                    var tempY = item.yPosition;

                    item.xPosition = lastXPosition;
                    lastXPosition  = tempX;

                    item.yPosition = lastYPosition;
                    lastYPosition = tempY;
                    writer.PrintInConsole('@', item.xPosition, item.yPosition);
                    writer.PrintInConsole(' ', lastXPosition, lastYPosition);
                }               
            }
        }        
    }
}