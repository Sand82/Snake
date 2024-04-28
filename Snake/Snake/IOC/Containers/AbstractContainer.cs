namespace Snake.IOC.Containers
{
    public abstract class AbstractContainer : IContainer
    {
        private Dictionary<Type, Type> mappings;

        private Dictionary<Type, KeyValuePair<Type, Func<object>>> mappingsWithCustomCreation;

        protected AbstractContainer()
        {
            mappings = new ();
            mappingsWithCustomCreation = new ();
        }

        public abstract void ConfigureServices();       

        public void CreateMapping<TInterfaceType, TImplementationType>()
        {
            CheckIsAssignableFrom<TInterfaceType, TImplementationType>();

            mappings[typeof(TInterfaceType)] = typeof(TImplementationType);
        }

        public void CreateMapping<TInterfaceType, TImplementationType>(Func<object> creationFunc)
        {
            CheckIsAssignableFrom<TInterfaceType, TImplementationType>();

            mappingsWithCustomCreation[typeof(TInterfaceType)] = new KeyValuePair<Type, Func<object>>(typeof(TImplementationType), creationFunc);
        }

        public Type GetMapping(Type interfaceType)
        {
            if (!mappings.ContainsKey(interfaceType))
            {
                return null!;
            }

            return mappings[interfaceType];
        }

        public KeyValuePair<Type, Func<object>> GetCustomMapping(Type interfaceType)
        {
            return mappingsWithCustomCreation[interfaceType];
        }

        private void CheckIsAssignableFrom<TInterfaceType, TImplementationType>()
        {
            if (!typeof(TInterfaceType).IsAssignableFrom(typeof(TImplementationType)))
            {
                throw new ArgumentException($"{typeof(TInterfaceType).Name} is not assignable from {typeof(TImplementationType).Name}.");
            }
        }
    }
}
