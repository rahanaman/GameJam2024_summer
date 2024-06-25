using MarsDonalds.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MarsDonalds
{
    public struct OrderStartEvent
    {
        static OrderStartEvent e;

        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }
    }
    public struct OrderTimeEvent
    {
        static OrderTimeEvent e;
        public int value;

        public static void Trigger(int a)
        {
            e.value = a;
            EventManager.TriggerEvent(e);
        }
    }
    public struct OrderSubmitEvent
    {
        static OrderSubmitEvent e;
        
        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }
    }
    public struct OrderCancelEvent
    {
        static OrderCancelEvent e;

        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }
    }
    public struct OrderCompleteEvent
    {
        static OrderCompleteEvent e;
        int reward;
        public static void Trigger(int reward)
        {
            e.reward = reward;
            EventManager.TriggerEvent(e);
        }
    }

    /// <summary>
    /// 들어오는 주문을 control
    /// 현재 만들어야 하는 음식이 뭔지 등등
    /// </summary>
    public class Order : MonoBehaviour, 
        IEventListener<StageStartEvent>, IEventListener<StageTimeEvent>, IEventListener<StageEndEvent>,
        IEventListener<OrderSubmitEvent>
    {
        private static readonly int[] orderMenuCount = { 1, 1, 1, 2, 2, 2, 2, 2, 3, 3 };

        public class OrderData
        {
            List<int> menuData;
            List<int> extraSource;
            List<int> extraDrink;

            int passedTime;
            int timeLimit;

            public OrderData()
            {
                int menuCount = orderMenuCount[Random.Range(0, orderMenuCount.Length)];
                menuData = new List<int>(menuCount);
                extraSource = new List<int>(3);
                for (int i = 1; i <= 3; ++i) {
                    if (Random.Range(0, 100) < 30) {
                        extraSource.Add(i);
                    }
                }
                extraDrink = new List<int>(3);
                for (int i = 1; i <= 3; ++i) {
                    if (Random.Range(0, 100) < 80) {
                        extraDrink.Add(i);
                    }
                }
                passedTime = 0;
                timeLimit = 100;
            }

            public bool IsSubmittable() => passedTime < timeLimit;
            public void TimePassed() => passedTime++;

            public int PassedTime => passedTime;
            public int RemainTime => timeLimit - passedTime;
        }
        public static Order Instance { get; private set; } = null;
        public bool IsListening => throw new System.NotImplementedException();

        public List<Data.Order> orderTable;
        private int totalWeight;

        private OrderData _current;
        private bool _isSubmit = false;

        public void EventStart()
        {
            this.EventStartListening<StageStartEvent>();
            this.EventStartListening<StageTimeEvent>();
            this.EventStartListening<StageEndEvent>();
            this.EventStartListening<OrderSubmitEvent>();
        }
        public void EventStop()
        {
            this.EventStopListening<StageStartEvent>();
            this.EventStopListening<StageTimeEvent>();
            this.EventStopListening<StageEndEvent>();
            this.EventStopListening<OrderSubmitEvent>();
        }
        public void OnEvent(StageStartEvent e)
        {
            if (e.stageIndex > 0) {
                orderTable = Data.Order.OrderList.
                    Where(o => o.openDate <= e.stageIndex).
                    OrderBy(x => x.weight).
                    ToList();
                totalWeight = 0;
                foreach (Data.Order order in orderTable) {
                    totalWeight += order.weight;
                }
            }
        }
        public void OnEvent(StageTimeEvent e)
        {
            throw new System.NotImplementedException();
        }
        public void OnEvent(StageEndEvent e)
        {
            StopAllCoroutines();
        }
        public void OnEvent(OrderSubmitEvent e)
        {
            // 제출함.
            _isSubmit = true;
            OrderCompleteEvent.Trigger(100);
            _current = null;
        }

        private IEnumerator Routine()
        {
            WaitForSeconds waitForSecond = new WaitForSeconds(1f);
            while (Stage.Instance.IsPlay) {
                _current = new OrderData();
                _isSubmit = false;
                while (_isSubmit == false &&
                    _current.IsSubmittable()) {
                    _current.TimePassed();
                    OrderTimeEvent.Trigger(_current.PassedTime);
                    yield return waitForSecond;
                }
                if (_current == null) continue;
                // 주문 제한 시간 초과
                OrderCancelEvent.Trigger();
            }
        }

        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            StartCoroutine(Routine());
        }
        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
