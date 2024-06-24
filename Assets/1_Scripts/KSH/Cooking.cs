using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    public class Cooking : MonoBehaviour
    {
        public static Cooking Instance { get; private set; } = null;

        private Order _order;
        private float _tick;
        private Stage _stage;

        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
                Init();
            }
            else {
                Destroy(gameObject);
            }
        }

        void Init()
        {
            // 데이터 로드


        }

        private void Start()
        {
            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            if (_order.isComplete) {


            }
            while (_tick < _stage.timeLimit) {
                if (_order.isComplete) {
                    // 새로운 order 갱신



                }
                _tick += Time.deltaTime;
                yield return null;
            }
            // 스테이지 종료
        }
    }
}
