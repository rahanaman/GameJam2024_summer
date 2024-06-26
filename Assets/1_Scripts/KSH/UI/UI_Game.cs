using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarsDonalds
{
    public class UI_Game : MonoBehaviour, IEventListener<MoneyEvent>, IEventListener<StageTimeEvent>
    {
        [SerializeField] private Image _image_StageTime;
        [SerializeField] private TextMeshProUGUI _text_Money;

        public bool IsListening => throw new System.NotImplementedException();

        private void Start()
        {
            _text_Money.SetText($"$ {GameManager.Instance.Money}");
            _image_StageTime.fillAmount = 1;
        }
        public void EventStart()
        {
            this.EventStartListening<MoneyEvent>();
            this.EventStartListening<StageTimeEvent>();
        }
        public void EventStop()
        {
            this.EventStopListening<MoneyEvent>();
            this.EventStopListening<StageTimeEvent>();
        }
        public void OnEvent(MoneyEvent e)
        {
            _text_Money.SetText($"$ {e.value}");
        }

        public void OnEvent(StageTimeEvent e)
        {
            _image_StageTime.fillAmount = (float)(e.max - e.current) / e.max;
        }

        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
