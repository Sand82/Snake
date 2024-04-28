namespace Snake.IOC.Containers
{
    public interface IContainer
    {
        public void ConfigureServices();

        public void CreateMapping<TInterfaceType, TImplementationType>();

        public void CreateMapping<TInterfaceType, TImplementationType>(Func<object> creationFunc);

        public Type GetMapping(Type interfaceType);

        public KeyValuePair<Type, Func<object>> GetCustomMapping(Type interfaceType);
    }
}
