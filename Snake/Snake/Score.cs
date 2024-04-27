using Snake.Contracts;

namespace Snake
{
    public class Score : Coordinates, IScore
    {
        private int score = 0;

        private const int xScorePosition = 0;

        private const int yScorePosition = 31;     

        private int foodPoints = 100;

        public int turnPoints = 10;

        public Score()
            : base(xScorePosition, yScorePosition)
        { }        

        public void AddScore(int points)
        {
            score += points;
        }

        public int GetScore()
        {
            return score;
        }

        public int GetXScorePosition()
        {
            return xScorePosition;
        }

        public int GetYScorePosition()
        {
            return xScorePosition;
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
