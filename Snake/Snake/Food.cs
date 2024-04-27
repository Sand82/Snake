using Snake.Contracts;

namespace Snake
{
    public class Food : Coordinates
    {
        public char foodSymbol = '#';

        public Food(int xPosition, int yPosition) 
            : base(xPosition, yPosition)
        {}
    }
}
