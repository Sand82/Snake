using Snake.IOC.Attributes;
using Snake.IOC.Containers;
using System.Reflection;

namespace Snake.IOC
{
    public class Injector
    {
        private IContainer container;

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

                ParameterInfo[] parametersInfo = constructor.GetParameters();
                object[] constructorParamObjects = new object[parametersInfo.Length];
                int i = 0;

                foreach (ParameterInfo parameterInfo in parametersInfo) 
                {
                   Type implementationType = container.GetMapping(parameterInfo.ParameterType);

                    MethodInfo injectMethod = typeof(Injector).GetMethod("Inject")!;
                    injectMethod = injectMethod.MakeGenericMethod(implementationType);

                    object? implementationInstance = injectMethod.Invoke(this, new object[] { });
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
    }
}
