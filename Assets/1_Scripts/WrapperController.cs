using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MarsDonalds
{

    public struct WrapStartEvent
    {
        static WrapStartEvent e;
        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }

    }
    public struct WrapEndEvent
    {
        static WrapEndEvent e;
        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }

    }
    public class WrapperController : MonoBehaviour, IEventListener<WrapStartEvent>, IEventListener<StageTimeEvent>
    {

        [SerializeField] private Image _bar;

        public CookData Data { get; private set; }
        public bool IsCooking { get; private set; } = false;
        public bool IsAvailable { get { return Data == null; } }
        private int _currentTime;
        private int _startTime;

        private int _cookTime = 1;
        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
            this.EventStartListening<StageTimeEvent>();
            this.EventStartListening<WrapStartEvent>();
        }

        public void EventStop()
        {
            this.EventStopListening<StageTimeEvent>();
            this.EventStopListening<WrapStartEvent>();
        }

        public void OnEvent(StageTimeEvent e)
        {
            _currentTime = e.current;
            if (!IsCooking) return;
            if (_currentTime - _startTime <= _cookTime)
            {
                _bar.fillAmount = (float)(_currentTime - _startTime) / _cookTime;
            }
            else
            {
                IsCooking = false;
                _bar.fillAmount = 0;
                WrapEndEvent.Trigger();
            }
        }

        public void OnEvent(WrapStartEvent e)
        {
            _startTime = _currentTime;
            IsCooking = true;
        }



        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();

        public void SetCookData(CookData d = null)
        {
            Data = d;
        }
    }
}
