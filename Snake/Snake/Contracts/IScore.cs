using static System.Formats.Asn1.AsnWriter;

namespace Snake.Contracts
{
    public interface IScore
    {
        public void AddScore(int points);

        public int GetScore();        

        public void PrintScoreInPosition();

        public int GetFoodPoints();

        public int GetTurnPoints();
    }
}
