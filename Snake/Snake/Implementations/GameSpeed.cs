using Snake.Contracts;
using Snake.DI.Attributes;

namespace Snake.Implementations
{
    public class GameSpeed : IGameSpeed
    {
        private int speed = 200;

        [Inject]
        public GameSpeed()
        { }

        public int GetGameSpeed()
        {
            return speed;
        }

        public void SetGameSpeed(int score)
        {
            if (score >= 10000 && score < 15000)
            {
                speed = 150;
            }
            else if (score >= 15000 && score < 20000)
            {
                speed = 100;
            }
            else if (score >= 20000 && score < 30000)
            {
                speed = 50;
            }
            else if (score >= 30000)
            {
                speed = 20;
            }
        }
    }
}
