using System;
using System.Collections.Generic;
using System.Linq;

public class SimpleIoCContainer : IDisposable
{
    private readonly Dictionary<(Type, string?), Func<object>> _registrations = new();
    private readonly List<object> _instances = new();

    // Register with a name
    public void Register<TService>(Func<object> factory, string? name = null)
    {
        _registrations[(typeof(TService), name)] = factory;
    }

    // Resolve by type and name
    public TService Resolve<TService>(string? name = null)
    {
        if (_registrations.TryGetValue((typeof(TService), name), out var factory))
        {
            var instance = factory();
            if (instance is IDisposable disposable)
            {
                _instances.Add(disposable);
            }
            return (TService)instance;
        }

        throw new InvalidOperationException(
            $"Service of type {typeof(TService)} with name '{name}' is not registered.");
    }

    public object Resolve(Type type, string name = null)
    {
        if (_registrations.TryGetValue((type, name), out var factory))
        {
            var instance = factory();
            if (instance is IDisposable disposable)
            {
                _instances.Add(disposable);
            }
            return instance;
        }

        throw new InvalidOperationException(
           $"Service of type {type} with name '{name}' is not registered.");
    }


    // Dispose all disposable instances
    public void Dispose()
    {
        foreach (var instance in _instances.OfType<IDisposable>())
        {
            instance.Dispose();
        }
        _instances.Clear();
    }
}
