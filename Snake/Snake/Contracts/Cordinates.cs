
namespace Snake.Contracts
{
    public abstract class Cordinates
    {
        public Cordinates(int xPosition, int yPosition)
        {
            this.xPosition = xPosition;
            this.yPosition = yPosition;
        }

        public int xPosition { get; set; }

        public int yPosition { get; set; }
    }
}
