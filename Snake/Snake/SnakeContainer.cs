using Snake.Contracts;
using Snake.IOC.Containers;
using Snake.Implementations;

namespace Snake
{
    public class SnakeContainer : AbstractContainer
    {
        public override void ConfigureServices()
        {
            CreateMapping<IWriter, Writer>();
            CreateMapping<ISnake, Implementations.Snake>();
            CreateMapping<IScore, Score>();
            CreateMapping<IGameSpeed, GameSpeed>();
            CreateMapping<IBoard, Board>();
            CreateMapping<IGame, Game>();
            CreateMapping<Engine, Engine>();            
        }
    }
}
