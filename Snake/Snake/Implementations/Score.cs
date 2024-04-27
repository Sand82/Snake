using Snake.Contracts;
using Snake.DI.Attributes;

namespace Snake.Implementations
{
    public class Score : Coordinates, IScore
    {
        private IWriter writer;

        private const int xScorePosition = 0;

        private const int yScorePosition = 31;

        private int foodPoints = 100;

        public int turnPoints = 10;

        private int score = 0;

        [Inject]
        public Score(IWriter writer)
            : base(xScorePosition, yScorePosition)
        {
            this.writer = writer;
        }

        public void AddScore(int points)
        {
            score += points;
        }

        public void PrintScoreInPosition()
        {
            writer.WriteInPosition(xScorePosition, yScorePosition, string.Format("Score is: {0}", score));
        }

        public int GetScore()
        {
            return score;
        }

        public int GetFoodPoints()
        {
            return foodPoints;
        }

        public int GetTurnPoints()
        {
            return turnPoints;
        }        
    }
}
