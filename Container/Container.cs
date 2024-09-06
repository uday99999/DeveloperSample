using System;
using System.Collections.Generic;

namespace DeveloperSample.Container
{
    public class Container
    {
        private readonly Dictionary<Type, Type> _bindings = new();

        public void Bind(Type interfaceType, Type implementationType)
        {
            if (!interfaceType.IsInterface)
            {
                throw new ArgumentException($"{interfaceType} is not an interface");
            }
            if (!implementationType.IsAssignableFrom(implementationType))
            {
                throw new ArgumentException($"{implementationType} does not implement the specified interface");
            }
            _bindings[interfaceType] = implementationType;
        }
        public T Get<T>() 
        {
            var type = typeof(T);
            if(_bindings.TryGetValue(type, out var implType))
            {
                return (T)Activator.CreateInstance(implType);
            }
            throw new ArgumentException();
        }
    }
}