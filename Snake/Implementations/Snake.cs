using Snake.Contracts;
using Snake.IOC.Attributes;

namespace Snake.Implementations
{  
    public class Snake : ISnake
    {
        private const int startingPositionX = 40;

        private const int startingPositionY = 15;

        private IWriter writer;

        private SnakePart? teal;

        private SnakePart head;

        private Dictionary<int, SnakePart> body { get; set; } = new();

        private int count = 1;

        [Inject]
        public Snake(IWriter writer)
        {
            this.writer = writer;

            if (head == null)
            {
                var head = new SnakePart(startingPositionX, startingPositionY);
                this.head = head;
                teal = head;
                body.Add(count, head);
            }
        }

        public void AddBodyElement(int xPosition, int yPosition)
        {
            var bodyElement = new SnakePart(xPosition, yPosition);

            count++;
            teal = bodyElement;
            body.Add(count, bodyElement);
        }

        public SnakePart GetTeal()
        {
            return teal;
        }

        public SnakePart GetHead()
        {
            return head;
        }

        public bool CheckSnakeForSelfBite()
        {
            foreach (var snakePart in body.Skip(1))
            {
                if (head.xPosition == snakePart.Value.xPosition && head.yPosition == snakePart.Value.yPosition)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckNewFoodOverrideSnakeBody(int foodXPosition, int foodYPosition)
        {
            return body.Any(sb => sb.Value.xPosition == foodXPosition && sb.Value.yPosition == foodYPosition);
        }

        public void MoveSnake(int xValue, int yValue)
        {
            writer.PrintInConsole(' ', teal.xPosition, teal.yPosition);

            var lastXCoordinate = head.xPosition;
            var lastYCoordinate = head.yPosition;

            head.xPosition += xValue;
            head.yPosition += yValue;

            writer.PrintInConsole('@', head.xPosition, head.yPosition);

            for (int i = 2; i <= count; i++)
            {
                var bodyPart = body[i];

                (lastXCoordinate, bodyPart.xPosition) = MoveInPosition(bodyPart.xPosition, lastXCoordinate);
                (lastYCoordinate, bodyPart.yPosition) = MoveInPosition(bodyPart.yPosition, lastYCoordinate);

                writer.PrintInConsole('@', bodyPart.xPosition, bodyPart.yPosition);
            }
        }

        private (int, int) MoveInPosition(int currentPosition, int lastPosition)
        {
            var temp = currentPosition;

            currentPosition = lastPosition;

            lastPosition = temp;

            return (lastPosition, currentPosition);
        }
    }
}