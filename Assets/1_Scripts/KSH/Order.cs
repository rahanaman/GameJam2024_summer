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

    /// <summary>
    /// ������ �ֹ��� control
    /// ���� ������ �ϴ� ������ ���� ���
    /// </summary>
    public class Order : MonoBehaviour, IEventListener<StageStartEvent>, IEventListener<StageTimeEvent>
    {
        public static Order Instance { get; private set; } = null;
        public bool IsListening => throw new System.NotImplementedException();

        public List<Data.Order> orderTable;
        private int totalWeight;

        public void EventStart()
        {
            this.EventStartListening<StageStartEvent>();
            this.EventStartListening<StageTimeEvent>();
        }
        public void EventStop()
        {
            this.EventStopListening<StageStartEvent>();
            this.EventStopListening<StageTimeEvent>();
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

        public Data.Order GetOrder()
        {
            int random = Random.Range(0, totalWeight);
            for (int i = 0; i < orderTable.Count; ++i) {
                if (random < orderTable[i].weight) {
                    return orderTable[i];
                }
                random -= orderTable[i].weight;
            }
            return null;    // ������� ���� �ȵ�
        }
        public List<Data.Order> GetOrders(int maxCount)
        {
            int random = Random.Range(0, maxCount) + 1; // �ֹ� ������ ���� Ȯ�� ����ġ�� �ִٸ� ���⼭ �����ϱ�
            List<Data.Order> result = new List<Data.Order>();
            for (int i = 0; i < random; ++i) {
                result.Add(GetOrder());
            }
            return result;
        }

        public void StartOrder()
        {


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
            // �ֹ� ���� �ð� �ʰ�
        }

        private void Awake()
        {
            Instance = this;
        }
        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();


    }
}
