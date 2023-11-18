using Snake.Contracts;

namespace Snake
{
    public class Score : Cordinates
    {
        private int score = 0;

        private const int xScorePosition = 0;

        private const int yScorePosition = 31;

        public Score() 
            : base(xScorePosition, yScorePosition)
        {}

        public int FoodPoints { get; private set; } = 100;

        public int TurnPoints { get; set; } = 10;

        public void AddScore(int points)
        {
            score += points;
        }        

        public string GetScore()
        {
            return score.ToString();
        }
    }
}
