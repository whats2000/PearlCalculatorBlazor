using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearlCalculatorBlazor.Managers
{
    public class EventManager
    {
        private static EventManager _instance;
        public static EventManager Instance => _instance ??= new EventManager();

        interface IEventHandlerWrapper
        {
            public void Invoke(object sender, EventArgs e);
        }

        class EventHanderWrapper<T> : IEventHandlerWrapper where T : EventArgs
        {

            public event CEventHandler<T> Handler;

            public void Invoke(object sender, EventArgs e) => Handler?.Invoke(sender, (T)e);
        }


        Dictionary<string, Dictionary<Type, IEventHandlerWrapper>> _eventCon;


        private EventManager()
        {
            _eventCon = new();
        }

        public void AddListener<T>(string eventKey, CEventHandler<T> eventHandler) where T : EventArgs
        {
            if (string.IsNullOrEmpty(eventKey) || string.IsNullOrWhiteSpace(eventKey))
                throw new ArgumentNullException(nameof(eventKey));

            if (eventHandler is null)
                throw new ArgumentNullException(nameof(eventHandler));

            if (!_eventCon.TryGetValue(eventKey, out var handlerDict))
            {
                handlerDict = new();
                _eventCon.Add(eventKey, handlerDict);
            }

            EventHanderWrapper<T> wrapper;
            if (!handlerDict.TryGetValue(typeof(T), out var w))
                handlerDict.Add(typeof(T), wrapper = new());
            else
                wrapper = (EventHanderWrapper<T>)w;

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
                var wrapper = (EventHanderWrapper<T>)w;
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
            { 
                wrapper?.Invoke(sender, args);
            }
        }
    }

    public delegate void CEventHandler<in T>(object sender, T e);
}
