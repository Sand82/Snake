namespace Snake.Contracts
{
    public interface ISnake
    {
        public void AddBodyElement(int xPosition, int yPosition);

        public void MoveSnake(int xValue, int yValue);

        public bool CheckSnakeForSelfBite();

        public bool CheckNewFoodOverrideSnakeBody(int foodXPosition, int foodYPosition);

        public SnakePart GetTeal();

        public SnakePart GetHead();
    }
}
