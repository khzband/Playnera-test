using UnityEngine;
using System;
using System.Collections.Generic;

public class ServiceLocator
{
    private ServiceLocator()
    {

    }

    private readonly Dictionary<string, IService> _services = new Dictionary<string, IService>();

    public static ServiceLocator Instance { get; private set; }

    public static void Init()
    {
        Instance = new ServiceLocator();
    }

    /// <summary>
    /// ¬озвращает сервис нужного типа
    /// </summary>
    public T Get<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            Debug.Log($"Service {key} is not registered with the {GetType().Name}");
            throw new InvalidOperationException();
        }

        return (T)_services[key];
    }

    /// <summary>
    /// –егистрирует сервис в текущем сервис локаторе
    /// </summary>
    /// <typeparam name="T">“ип сервиса</typeparam>
    /// <param name="service">Ёкземл€р сервиса</param>
    public void Register<T>(T service) where T : IService
    {
        string key = typeof(T).Name;
        if (_services.ContainsKey(key))
        {
            Debug.Log($"Service {key} is already registered with the {GetType().Name}");
            return;
        }

        _services.Add(key, service);
    }

    /// <summary>
    /// ”бирает сервис из текущего сервис локатора
    /// </summary>
    /// <typeparam name="T">“ип сервиса</typeparam>
    public void Unregister<T>() where T : IService
    {
        string key = typeof(T).Name;
        if (!_services.ContainsKey(key))
        {
            Debug.Log($"Attemted to unregister service {key}, which is not registered with the {GetType().Name}");
            return;
        }

        _services.Remove(key);

    }


}
