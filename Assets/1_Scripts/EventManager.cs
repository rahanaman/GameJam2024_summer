using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    public struct GenericData<T>
    {
        public T value;
    }
    public struct GenericData<T, K>
    {
        public T value1;
        public K value2;
    }
    public struct GenericData<T, K, L>
    {
        public T value1;
        public K value2;
        public L value3;
    }
    public struct GenericData<T, K, L, F>
    {
        public T value1;
        public K value2;
        public L value3;
        public F value4;
    }
    public interface IEventListenerBase
    {
        bool IsListening { get; }
        void EventStart();
        void EventStop();
    };
    public interface IEventListener<T> : IEventListenerBase
    {
        void OnEvent(T e);
    }
    public static class EventRegister
    {
        public delegate void Delegate<T>(T eventType);

        public static void EventStartListening<EventType>(this IEventListener<EventType> caller) where EventType : struct
        {
            EventManager.AddListener<EventType>(caller);
        }

        public static void EventStopListening<EventType>(this IEventListener<EventType> caller) where EventType : struct
        {
            EventManager.RemoveListener<EventType>(caller);
        }
    }

    public class MMEventListenerWrapper<TOwner, TTarget, TEvent> : IEventListener<TEvent>, IDisposable where TEvent : struct
    {
        private Action<TTarget> _callback;

        private TOwner _owner;

        public bool IsListening => throw new NotImplementedException();

        public MMEventListenerWrapper(TOwner owner, Action<TTarget> callback)
        {
            _owner = owner;
            _callback = callback;
            RegisterCallbacks(true);
        }

        public void Dispose()
        {
            RegisterCallbacks(false);
            _callback = null;
        }
        public void EventStart() { }
        public void EventStop() { }
        protected virtual TTarget OnTrigger(TEvent eventType) => default;
        public void OnEvent(TEvent eventType)
        {
            var item = OnTrigger(eventType);
            _callback?.Invoke(item);
        }

        private void RegisterCallbacks(bool b)
        {
            if (b) {
                this.EventStartListening<TEvent>();
            }
            else {
                this.EventStopListening<TEvent>();
            }
        }
    }

    [ExecuteAlways]
    public class EventManager
    {
        private static Dictionary<Type, List<IEventListenerBase>> _subscribersList;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        static void Init()
        {
            _subscribersList = new Dictionary<Type, List<IEventListenerBase>>();
        }
        static EventManager()
        {
            _subscribersList = new Dictionary<Type, List<IEventListenerBase>>();
        }
        public static void AddListener<T>(IEventListener<T> listener) where T : struct
        {
            Type eventType = typeof(T);

            if (!_subscribersList.ContainsKey(eventType)) {
                _subscribersList[eventType] = new List<IEventListenerBase>();
            }

            if (!SubscriptionExists(eventType, listener)) {
                _subscribersList[eventType].Add(listener);
            }
        }
        public static void RemoveListener<T>(IEventListener<T> listener) where T : struct
        {
            Type eventType = typeof(T);

            if (!_subscribersList.ContainsKey(eventType))
                return;

            List<IEventListenerBase> subscriberList = _subscribersList[eventType];

            for (int i = subscriberList.Count - 1; i >= 0; i--) {
                if (subscriberList[i] == listener) {
                    subscriberList.Remove(subscriberList[i]);

                    if (subscriberList.Count == 0) {
                        _subscribersList.Remove(eventType);
                    }

                    return;
                }
            }
        }
        public static void TriggerEvent<T>(T newEvent) where T : struct
        {
            List<IEventListenerBase> list;
            if (!_subscribersList.TryGetValue(typeof(T), out list))
                return;

            for (int i = list.Count - 1; i >= 0; i--) {
                (list[i] as IEventListener<T>).OnEvent(newEvent);
            }
        }
        private static bool SubscriptionExists(Type type, IEventListenerBase receiver)
        {
            List<IEventListenerBase> receivers;

            if (!_subscribersList.TryGetValue(type, out receivers)) return false;

            bool exists = false;

            for (int i = receivers.Count - 1; i >= 0; i--) {
                if (receivers[i] == receiver) {
                    exists = true;
                    break;
                }
            }

            return exists;
        }
    }
}