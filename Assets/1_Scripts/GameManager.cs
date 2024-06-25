using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    public struct GameStartEvent
    {
        static GameStartEvent e;

        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }
    }
    public struct MoneyEvent
    {
        static MoneyEvent e;
        public int value;
        public static void Trigger(int v)
        {
            e.value = v;
            EventManager.TriggerEvent(e);
        }
    }
    public class GameManager : MonoBehaviour, IEventListener<GameStartEvent>, IEventListener<MoneyEvent>
    {

        public static GameManager instance { get; private set; } = null;
        public bool IsListening => throw new System.NotImplementedException();

        private int _startMoney = 10000;
        public int Money { get; private set; }

        public void EventStart()
        {
            this.EventStartListening<GameStartEvent>();
            this.EventStartListening<MoneyEvent>();

        }

        public void EventStop()
        {
            this.EventStopListening<GameStartEvent>();
            this.EventStopListening<MoneyEvent>();
        }

        public void OnEvent(GameStartEvent e)
        {
            Money = _startMoney;
        }

        public void OnEvent(MoneyEvent e)
        {
            Money += e.value;
        }

        private void Awake()
        {
            if(instance != null)
            {
                Destroy(this.gameObject);
            }
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }

}
