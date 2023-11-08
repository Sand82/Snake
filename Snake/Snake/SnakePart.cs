namespace Snake
{
    public class SnakePart
    {
        public SnakePart(int xPosition, int yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }

        public int xPosition { get; set; }

        public int yPosition { get; set; }
    }
}
