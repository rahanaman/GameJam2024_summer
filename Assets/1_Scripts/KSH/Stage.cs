using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UGS;
using Unity.VisualScripting;
using UnityEngine;

namespace MarsDonalds
{
    public struct StageTimeEvent
    {
        static StageTimeEvent e;
        public int value;

        public static void Trigger(int a)
        {
            e.value = a;
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


    public class Stage : MonoBehaviour
    {
        private class OrderData
        {
            public List<Data.Order> orders;
            public int totalWeight;
            public OrderData(List<Data.Order> orders, int totalWeight)
            {
                this.orders = orders;
                this.totalWeight = totalWeight;
            }
            public Data.Order GetOrder()
            {
                int random = Random.Range(0, totalWeight);
                for(int i = 0; i < orders.Count; ++i) {
                    if(random < orders[i].weight) {
                        return orders[i];
                    }
                    random -= orders[i].weight;
                }
                return null;    // 여기까지 가면 안됨
            }
            public List<Data.Order> GetOrders(int maxCount)
            {
                int random = Random.Range(0, maxCount) + 1; // 주문 개수에 대한 확률 가중치가 있다면 여기서 적용하기
                List<Data.Order> result = new List<Data.Order>();
                for(int i = 0; i < random; ++i) {
                    result.Add(GetOrder());
                }
                return result;
            }
            public static OrderData Build(int openDate)
            {
                var orders = Data.Order.OrderList.
                    Where(o => o.openDate <= openDate).
                    OrderBy(x => x.weight).
                    ToList();
                int totalWeight = 0;
                foreach (Data.Order order in orders) {
                    totalWeight += order.weight;
                }
                return new OrderData(orders, totalWeight);
            }
        }

        public static Stage Instance { get; private set; } = null;
        public int stageIndex = 1;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            UnityGoogleSheet.LoadAllData();
            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            int currentTime = 0;
            WaitForSeconds waitForSecond = new WaitForSeconds(1f);
            OrderData stageOrderData = OrderData.Build(stageIndex);
            foreach(Data.Order order in stageOrderData.orders) {
                Debug.Log($"{order.index} : {order.orderName} weight : {order.weight}");
            }
            for(int i = 0; i < 5; ++i) {
                Data.Order order = stageOrderData.GetOrder();
                Debug.Log($"{order.index} : {order.orderName}");
            }
            // 스테이지 시작
            while(currentTime < 300) {
                StageTimeEvent.Trigger(currentTime++);
                yield return waitForSecond;
            }
            // 스테이지 종료
        }
    }
}
