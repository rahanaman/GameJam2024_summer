using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MarsDonalds
{

    public struct CookStartEvent
    {
        static CookStartEvent e;
        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }
        
    }

    public struct CookEndEvent
    {
        static CookEndEvent e;
        public int value;

        public static void Trigger(int v)
        {
            e.value = v;
            EventManager.TriggerEvent(e);
        }
    }
    public class CookBoxController : MonoBehaviour, IEventListener<StageTimeEvent>, IEventListener<CookStartEvent>, IEventListener<CookEndEvent>
    {
        [SerializeField] private int _cookType;

        [SerializeField] private Image _bar;

        public CookData Data { get; private set; }
        public bool IsCooking { get; private set; } = false;
        public bool IsAvailable { get { return Data == null; } }
        private int _currentTime;
        private int _startTime;

        private int _cookTime = 3;
        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
            this.EventStartListening<StageTimeEvent>();
            this.EventStartListening<CookEndEvent>();
            this.EventStartListening<CookStartEvent>();
        }

        public void EventStop()
        {
            this.EventStopListening<StageTimeEvent>();
            this.EventStopListening<CookEndEvent>();
            this.EventStopListening<CookStartEvent>();
        }

        public void OnEvent(StageTimeEvent e)
        {
            _currentTime = e.value;
            if (!IsCooking) return;
            if(_currentTime-_startTime <= _cookTime)
            {
                _bar.fillAmount = (float)(_currentTime - _startTime)/_cookTime;
            }
            else
            {
                CookEndEvent.Trigger(_cookType);
            }
        }

        public void OnEvent(CookStartEvent e)
        {
            _startTime = _currentTime;
            IsCooking = true;
            // ���� ��ȭ - cookData �����ϱ�
        }

        public void OnEvent(CookEndEvent e)
        {
            IsCooking = false;
            _bar.fillAmount = 0;
            
        }

        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();

        public void SetCookData(CookData d=null)
        {
            Data = d;
        }
    }
}
