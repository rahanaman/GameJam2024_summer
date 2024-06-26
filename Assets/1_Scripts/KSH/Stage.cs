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
        public int value;

        public static void Trigger(int a)
        {
            e.value = a;
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
    public class Stage : MonoBehaviour
    {

        private int _stageTime = 300;
        public static Stage Instance { get; private set; } = null;
        public int stageIndex = 1;
        public bool IsPlay = true;
        private void Awake()
        {
            Instance = this;
        }
        private void Start()
        {
            UnityGoogleSheet.LoadAllData();
            StartCoroutine(Routine());
        }

        public bool Debugs;
        private void Update()
        {
            if (Input.GetMouseButtonDown(0)) {
                if (Debugs) {
                    OrderCompleteEvent.Trigger(1);
                }
                else {
                    OrderCancelEvent.Trigger();
                }
                
            }
        }

        private IEnumerator Routine()
        {
            int currentTime = 0;
            WaitForSeconds waitForSecond = new WaitForSeconds(1f);
            // 스테이지 시작
            StageStartEvent.Trigger(stageIndex);
            while(currentTime < _stageTime) {
                StageTimeEvent.Trigger(currentTime++);
                yield return waitForSecond;
            }
            // 스테이지 종료
            StageEndEvent.Trigger();
        }
    }
}
