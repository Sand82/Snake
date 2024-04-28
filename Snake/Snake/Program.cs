using Snake.IOC;
using Snake.IOC.Containers;

namespace Snake
{ 
    public class Program
    { 
        public static void Main()
        {     
            IContainer container = new SnakeContainer();
            container.ConfigureServices();

            Injector injector = new Injector(container);
            Engine engine = injector.Inject<Engine>();  
            
            engine.StartGame();
        }       
    }
}