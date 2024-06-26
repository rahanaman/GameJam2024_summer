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
        private int _stageTime = 300;
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

        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
