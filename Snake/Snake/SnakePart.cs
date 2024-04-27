using Snake.Contracts;

namespace Snake
{
    public class SnakePart : Coordinates
    {
        public SnakePart(int xPosition, int yPosition) 
            : base(xPosition, yPosition)
        {}

        public SnakePart Next { get; set; }
    }
}
