using Snake.Contracts;

namespace Snake
{
    public class Snake
    {
        private IWriter writer;

        private int count = 1;

        public Snake(IWriter writer)
        {
            this.writer = writer;

            if (Head == null)
            {
                var head = new SnakePart(40, 15);
                Head = head;
                Teal = head;
                Body.Add(count, head);
            }
        }

        public SnakePart Teal { get; private set; }

        public SnakePart Head { get; private set; }
        
        public Dictionary<int, SnakePart> Body { get; set; } = new();

        public void AddBodyElemen(int xPosition, int yPosition)
        {
            var bodyElement = new SnakePart(xPosition, yPosition);
            
            count++;
            Teal = bodyElement;
            Body.Add(count, bodyElement);
        }

        public void MoveSnake(int xValue, int yValue)
        {            
            writer.PrintInConsole(' ', Teal.xPosition, Teal.yPosition);            

            var lastXCordinate = Head.xPosition;
            var lastYCordinate = Head.yPosition;

            Head.xPosition += xValue;
            Head.yPosition += yValue;

            writer.PrintInConsole('@', Head.xPosition, Head.yPosition);

            for (int i = 2; i <= count; i++)
            {
                var bodyPart = Body[i];               

                (lastXCordinate, bodyPart.xPosition) = MoveInPosition(bodyPart.xPosition, lastXCordinate);
                (lastYCordinate, bodyPart.yPosition) = MoveInPosition(bodyPart.yPosition, lastYCordinate);

                writer.PrintInConsole('@', bodyPart.xPosition, bodyPart.yPosition);
            }            
        }

        private (int, int) MoveInPosition( int currentPosition, int lastPosition )
        {
            var temp = currentPosition;

            currentPosition = lastPosition;

            lastPosition = temp;

            return (lastPosition, currentPosition);            
        }
    }
}