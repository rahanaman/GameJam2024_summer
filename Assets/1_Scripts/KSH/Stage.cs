using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UGS;
using UnityEngine;

namespace MarsDonalds
{
    public struct StageStartEvent
    {
        static StageStartEvent e;
        public int stageIndex;

        public static void Trigger(int a)
        {
            e.stageIndex = a;
            EventManager.TriggerEvent(e);
        }
    }
    public struct StageTimeEvent
    {
        static StageTimeEvent e;
        public int current;
        public int max;

        public static void Trigger(int current, int max)
        {
            e.current = current;
            e.max = max;
            EventManager.TriggerEvent(e);
        }
    }
    public struct StageEndEvent
    {
        static StageEndEvent e;
        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }
    }
    /// <summary>
    /// �������� ���������� ��Ʈ��
    /// ���� ���ѽð�, �� ���
    /// </summary>
    public class Stage : MonoBehaviour, IEventListener<AnimationEndEvent>
    {
        public int ��ᰪ = 0;
        public int ��Ʈ�� = 0;
        public int ��Ⱚ = 0;
        public int ���� = 0;
        public int ���� = 0;
        public static Stage Instance { get; private set; } = null;

        public bool IsListening => throw new System.NotImplementedException();

        public bool IsPlay = true;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            StartCoroutine(Routine());
        }

        private bool _isEnd;
        private IEnumerator Routine()
        {
            int currentTime = 0;
            int stageTime = 10;
            int stageIndex = GameManager.Instance.Stage;
            ��Ʈ�� = stageIndex * 100;
            WaitForSeconds waitForSecond = new WaitForSeconds(1f);
            // �������� ����

            _isEnd = false;
            StartAnimationEvent.Trigger(stageIndex);
            yield return new WaitUntil(() => _isEnd == true);
            StageStartEvent.Trigger(stageIndex);
            while(currentTime <= stageTime) {
                StageTimeEvent.Trigger(currentTime++, stageTime);
                yield return waitForSecond;
            }
            // �������� ����
            ���� = ��Ʈ�� + ��Ⱚ + ��ᰪ;
            GameManager.Instance.Money -= ��Ʈ��;
            MoneyEvent.Trigger(GameManager.Instance.Money);
            StageEndEvent.Trigger();
        }

        public void OnEvent(AnimationEndEvent e)
        {
            _isEnd = true;
        }

        public void EventStart()
        {
            this.EventStartListening<AnimationEndEvent>();
        }

        public void EventStop()
        {
            this.EventStopListening<AnimationEndEvent>();
        }

        public void Use���(int value)
        {
            ��ᰪ += value;
            GameManager.Instance.Money -= value;
            MoneyEvent.Trigger(GameManager.Instance.Money);
        }

        public void ���(int value)
        {
            ��Ⱚ += value;
            GameManager.Instance.Money -= value;
            MoneyEvent.Trigger(GameManager.Instance.Money);
        }

        public void �Ǹ�(int value)
        {
            ���� += value;
            GameManager.Instance.Money += value;
            MoneyEvent.Trigger(GameManager.Instance.Money);
        }

        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
