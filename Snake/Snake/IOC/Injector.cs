using Snake.IOC.Attributes;
using Snake.IOC.Containers;
using System.Reflection;

namespace Snake.IOC
{
    public class Injector
    {
        private IContainer container;

        [Inject]
        public Injector(IContainer container)
        {
            this.container = container;
        }

        public TClass Inject<TClass>() 
        {
            if (!HasConstructorInjection<TClass>())
            {
                return (TClass)Activator.CreateInstance(typeof(TClass))!;
            }

            return CreateConstructorInjection<TClass>();
        }

        private TClass CreateConstructorInjection<TClass>()
        {
            ConstructorInfo[] constructors = typeof(TClass).GetConstructors();

            foreach (ConstructorInfo constructor in constructors) 
            {
                if (constructor.GetCustomAttribute(typeof(Inject), true) == null)
                {
                    continue;
                }

                ParameterInfo[] constructorParams = constructor.GetParameters();
                object[] constructorParamObjects = new object[constructorParams.Length];
                int i = 0;

                foreach (ParameterInfo paramInfo in constructorParams) 
                {
                    object implementationInstance = GetImplementation(paramInfo);

                    constructorParamObjects[i++] = implementationInstance!;
                }

                return (TClass) Activator.CreateInstance(typeof(TClass), constructorParamObjects)!;
            }

            return default!;
        }

        private bool HasConstructorInjection<TClass>()
        {
            return typeof(TClass).GetConstructors().Any(c => c.GetCustomAttributes(typeof(Inject), true).Any());
        }

        private object CallGenericMethod(Type type)
        {
            MethodInfo injectMethod = typeof(Injector).GetMethod("Inject")!;
            injectMethod = injectMethod.MakeGenericMethod(type);

            return injectMethod.Invoke(this, new object[] { })!;
        }

        private object GetImplementation(ParameterInfo paramInfo)
        {
            Type interfaceType = paramInfo.ParameterType;
            Type implementationType = container.GetMapping(interfaceType);

            object implementationInstance;

            if (implementationType == null)
            {
                var implementationPair = container.GetCustomMapping(interfaceType);
                implementationInstance = implementationPair.Value();
            }
            else
            {
                implementationInstance = CallGenericMethod(implementationType);
            }

            return implementationInstance;
        }
    }
}
