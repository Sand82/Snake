namespace Snake.IOC.Containers
{
    public interface IContainer
    {
        public void ConfigureServices();

        public void CreateMapping<TInterfaceType, TImplementationType>();

        public Type GetMapping(Type interfaceType);        
    }
}
