using Snake.Contracts;

namespace Snake
{
    public class GameSpeed 
    {
        public int Speed { get; private set; } = 200;
        
        public void SetGameSpeed(int score)
        {
            if (score >= 10000 && score < 15000)
            {
                Speed = 150;
            }
            else if(score >= 15000 && score < 20000)
            {
                Speed = 100;
            }
            else if (score >= 20000 && score < 30000)
            {
                Speed = 50;
            }
            else if (score >= 30000)
            {
                Speed = 20;
            }
        }
    }
}
