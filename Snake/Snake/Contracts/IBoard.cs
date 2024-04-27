namespace Snake.Contracts
{
    public interface IBoard
    {
        public void DrawBorders(string message);

        public bool ValidateNextStep(int nextXPosition, int nextYPosition);
    }
}
