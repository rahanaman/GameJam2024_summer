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
    /// 전반적인 스테이지를 컨트롤
    /// 남은 제한시간, 돈 등등
    /// </summary>
    public class Stage : MonoBehaviour, IEventListener<AnimationEndEvent>
    {
        public int 재료값 = 0;
        public int 렌트값 = 0;
        public int 폐기값 = 0;
        public int 수입 = 0;
        public int 지출 = 0;
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
            렌트값 = stageIndex * 100;
            WaitForSeconds waitForSecond = new WaitForSeconds(1f);
            // 스테이지 시작

            _isEnd = false;
            StartAnimationEvent.Trigger(stageIndex);
            yield return new WaitUntil(() => _isEnd == true);
            StageStartEvent.Trigger(stageIndex);
            while(currentTime <= stageTime) {
                StageTimeEvent.Trigger(currentTime++, stageTime);
                yield return waitForSecond;
            }
            // 스테이지 종료
            지출 = 렌트값 + 폐기값 + 재료값;
            GameManager.Instance.Money -= 렌트값;
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

        public void Use재료(int value)
        {
            재료값 += value;
            GameManager.Instance.Money -= value;
            MoneyEvent.Trigger(GameManager.Instance.Money);
        }

        public void 폐기(int value)
        {
            폐기값 += value;
            GameManager.Instance.Money -= value;
            MoneyEvent.Trigger(GameManager.Instance.Money);
        }

        public void 판매(int value)
        {
            수입 += value;
            GameManager.Instance.Money += value;
            MoneyEvent.Trigger(GameManager.Instance.Money);
        }

        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
