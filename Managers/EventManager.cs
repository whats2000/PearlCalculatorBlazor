using System;
using System.Collections.Generic;

namespace PearlCalculatorBlazor.Managers;

public class EventManager
{
    private static EventManager _instance;
    private readonly Dictionary<string, Dictionary<Type, IEventHandlerWrapper>> _eventCon;

    private EventManager()
    {
        _eventCon = new Dictionary<string, Dictionary<Type, IEventHandlerWrapper>>();
    }

    public static EventManager Instance => _instance ??= new EventManager();

    public void AddListener<T>(string eventKey, CEventHandler<T> eventHandler) where T : EventArgs
    {
        if (string.IsNullOrEmpty(eventKey) || string.IsNullOrWhiteSpace(eventKey))
            throw new ArgumentNullException(nameof(eventKey));

        if (eventHandler is null)
            throw new ArgumentNullException(nameof(eventHandler));

        if (!_eventCon.TryGetValue(eventKey, out var handlerDict))
        {
            handlerDict = new Dictionary<Type, IEventHandlerWrapper>();
            _eventCon.Add(eventKey, handlerDict);
        }

        EventHandlerWrapper<T> wrapper;
        if (!handlerDict.TryGetValue(typeof(T), out var w))
            handlerDict.Add(typeof(T), wrapper = new EventHandlerWrapper<T>());
        else
            wrapper = (EventHandlerWrapper<T>)w;

        wrapper.Handler += eventHandler;
    }

    public void RemoveListener<T>(string eventKey, CEventHandler<T> eventHandler) where T : EventArgs
    {
        if (string.IsNullOrEmpty(eventKey) || string.IsNullOrWhiteSpace(eventKey))
            throw new ArgumentNullException(nameof(eventKey));

        if (eventHandler is null)
            throw new ArgumentNullException(nameof(eventHandler));

        if (!_eventCon.TryGetValue(eventKey, out var handlerDict)) return;

        if (handlerDict.TryGetValue(typeof(T), out var w))
        {
            var wrapper = (EventHandlerWrapper<T>)w;
            wrapper.Handler -= eventHandler;
        }
    }

    public void PublishEvent<T>(object sender, string eventKey, T args) where T : EventArgs
    {
        if (string.IsNullOrEmpty(eventKey) || string.IsNullOrWhiteSpace(eventKey))
            throw new ArgumentNullException(nameof(eventKey));

        if (args is null)
            throw new ArgumentNullException(nameof(args));

        if (_eventCon.TryGetValue(eventKey, out var dict) &&
            dict.TryGetValue(typeof(T), out var wrapper))
            wrapper?.Invoke(sender, args);
    }

    private interface IEventHandlerWrapper
    {
        public void Invoke(object sender, EventArgs e);
    }

    private class EventHandlerWrapper<T> : IEventHandlerWrapper where T : EventArgs
    {
        public void Invoke(object sender, EventArgs e)
        {
            Handler?.Invoke(sender, (T)e);
        }

        public event CEventHandler<T> Handler;
    }
}

public delegate void CEventHandler<in T>(object sender, T e);