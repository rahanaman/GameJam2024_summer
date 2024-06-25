using MarsDonalds.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MarsDonalds
{
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

    /// <summary>
    /// 들어오는 주문을 control
    /// 현재 만들어야 하는 음식이 뭔지 등등
    /// </summary>
    public class Order : MonoBehaviour, 
        IEventListener<StageStartEvent>, IEventListener<StageTimeEvent>, IEventListener<StageEndEvent>
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

            public bool IsRight()
        }
        
        public static Order Instance { get; private set; } = null;
        public bool IsListening => throw new System.NotImplementedException();

        public List<Data.Order> orderTable;
        private int totalWeight;
        private Coroutine _routine;

        public void EventStart()
        {
            this.EventStartListening<StageStartEvent>();
            this.EventStartListening<StageTimeEvent>();
            this.EventStartListening<StageEndEvent>();
        }
        public void EventStop()
        {
            this.EventStopListening<StageStartEvent>();
            this.EventStopListening<StageTimeEvent>();
            this.EventStopListening<StageEndEvent>();
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
            StopCoroutine(_routine);
        }
        public Data.Order GetOrder()
        {
            int random = Random.Range(0, totalWeight);
            for (int i = 0; i < orderTable.Count; ++i) {
                if (random < orderTable[i].weight) {
                    return orderTable[i];
                }
                random -= orderTable[i].weight;
            }
            return null;    // 여기까지 가면 안됨
        }
        public List<Data.Order> GetOrders()
        {
            int random = orderMenuCount[Random.Range(0, orderMenuCount.Length)]; // 주문 개수에 대한 확률 가중치가 있다면 여기서 적용하기
            List<Data.Order> result = new List<Data.Order>();
            for (int i = 0; i < random; ++i) {
                result.Add(GetOrder());
            }
            return result;
        }

        public void StartOrder()
        {
            List<Data.Order> orders = GetOrders();
            _routine = StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            int current = 0;
            int max = 100;
            WaitForSeconds waitForSecond = new WaitForSeconds(1f);
            while(current < max) {
                OrderTimeEvent.Trigger(current++);
                yield return waitForSecond;
            }
            // 주문 제한 시간 초과
            OrderCancelEvent.Trigger();
        }

        private void Awake()
        {
            Instance = this;
        }
        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
