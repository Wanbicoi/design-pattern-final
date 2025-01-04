using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReadDbforGeneration
{
    public class IoCContainer
    {
        private readonly Dictionary<Type, Func<ILifetime, object>> registeredTypes = new Dictionary<Type, Func<ILifetime, object>>();
        private readonly ContainerLifetime containerLifetime;

        public IoCContainer()
        {
            containerLifetime = new ContainerLifetime(t => registeredTypes[t]);
        }

        public void Register(Type interfaceType, Type implementationType)
        {
            registeredTypes[interfaceType] = FactoryFromType(implementationType);
        }

        public void RegisterSingleton(Type interfaceType, Type implementationType)
        {
            registeredTypes[interfaceType] = lifetime => lifetime.GetServiceAsSingleton(implementationType, FactoryFromType(implementationType));
        }

        public void RegisterPerScope(Type interfaceType, Type implementationType)
        {
            registeredTypes[interfaceType] = lifetime => lifetime.GetServicePerScope(implementationType, FactoryFromType(implementationType));
        }

        public object Resolve(Type type)
        {
            if (!registeredTypes.TryGetValue(type, out var factory))
                throw new InvalidOperationException($"Type {type.Name} not registered");

            return factory(containerLifetime);
        }

        private static Func<ILifetime, object> FactoryFromType(Type type)
        {
            var constructor = type.GetConstructors().FirstOrDefault();
            if (constructor == null)
                throw new InvalidOperationException($"No public constructor found for type {type.Name}");

            var arg = Expression.Parameter(typeof(ILifetime));
            return (Func<ILifetime, object>)Expression.Lambda(
                Expression.New(constructor, constructor.GetParameters().Select(param =>
                {
                    var resolve = new Func<ILifetime, object>(
                        lifetime => lifetime.GetServiceAsSingleton(param.ParameterType, null));
                    return Expression.Convert(Expression.Call(Expression.Constant(resolve.Target), resolve.Method, arg), param.ParameterType);
                })),
                arg).Compile();
        }
    }

    // defined how container manages the lifetime of objects
    public interface ILifetime
    {
        object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory);
        object GetServicePerScope(Type type, Func<ILifetime, object> factory);
    }



    public class ContainerLifetime : ILifetime
    {
        private readonly ConcurrentDictionary<Type, object> singletonCache = new ConcurrentDictionary<Type, object>();
        private readonly Func<Type, Func<ILifetime, object>> getFactory;

        public ContainerLifetime(Func<Type, Func<ILifetime, object>> getFactory)
        {
            getFactory = getFactory;
        }

        public object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory)
        {
            return singletonCache.GetOrAdd(type, _ => factory(this));
        }

        public object GetServicePerScope(Type type, Func<ILifetime, object> factory)
        {
            // Với Singleton, PerScope sẽ giống Singleton ở cấp độ container
            return GetServiceAsSingleton(type, factory);
        }
    }

    public class ScopeLifetime : ILifetime
    {
        private readonly ContainerLifetime containerLifetime;
        private readonly Dictionary<Type, object> perScopeCache = new Dictionary<Type, object>();

        public ScopeLifetime(ContainerLifetime containerLifetime)
        {
            containerLifetime = containerLifetime;
        }

        public object GetServiceAsSingleton(Type type, Func<ILifetime, object> factory)
        {
            return containerLifetime.GetServiceAsSingleton(type, factory);
        }

        public object GetServicePerScope(Type type, Func<ILifetime, object> factory)
        {
            if (!perScopeCache.TryGetValue(type, out var instance))
            {
                instance = factory(this);
                perScopeCache[type] = instance;
            }
            return instance;
        }
    }

}