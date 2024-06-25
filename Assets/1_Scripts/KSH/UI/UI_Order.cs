using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace MarsDonalds
{
    public class UI_Order : MonoBehaviour, 
        IEventListener<OrderStartEvent>, IEventListener<OrderTimeEvent>, IEventListener<OrderCancelEvent>, IEventListener<OrderCompleteEvent>
    {
        [SerializeField] private Image _image_Time;
        [SerializeField] private TextMeshProUGUI _text_Order;
        [SerializeField] private RectTransform _notice;
        [SerializeField] private GameObject _fail_1;
        [SerializeField] private GameObject _fail_2;
        [SerializeField] private Image _image_Complete;

        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
            this.EventStartListening<OrderStartEvent>();
            this.EventStartListening<OrderCancelEvent>();
            this.EventStartListening<OrderCompleteEvent>();
            this.EventStartListening<OrderTimeEvent>();
        }
        public void EventStop()
        {
            this.EventStopListening<OrderStartEvent>();
            this.EventStopListening<OrderCancelEvent>();
            this.EventStopListening<OrderCompleteEvent>();
            this.EventStopListening<OrderTimeEvent>();
        }
        public void OnEvent(OrderStartEvent e)
        {
            _notice.gameObject.SetActive(true);
            _fail_1.SetActive(false);
            _fail_2.SetActive(false);
            _notice.anchoredPosition = new Vector2(-250, -80);
            _image_Time.fillAmount = 1;
            _image_Complete.fillAmount = 0;
            StringBuilder sb = new StringBuilder();
            Order.OrderData orderData = e.orderData;
            for(int i = 0; i < orderData.menuData.Count; ++i) {
                switch (orderData.menuData[i].potatoID) {
                    case 1:
                        sb.Append("참치맛 ");
                        break;
                    case 2:
                        sb.Append("고구마맛 ");
                        break;
                    case 3:
                        sb.Append("계란맛 ");
                        break;
                }
                sb.Append(orderData.menuData[i].recipe.menuName);
                if(i < orderData.menuData.Count - 1) {
                    sb.Append(", ");
                }
            }
            sb.Append(" 만들어 주세요. ");
            if(orderData.extraSubMenu > 0) {
                sb.Append("햄버거 하나 추가");
            }
            if(orderData.extraDrink.Count > 0) {
                if (orderData.extraSubMenu > 0) {
                    sb.Append("하고 ");
                }
                sb.Append("음료는 ");
                for(int j = 0; j < orderData.extraDrink.Count; ++j) {
                    switch (orderData.extraDrink[j]) {
                        case 1:
                            sb.Append("제로콜라 하나");
                            break;
                        case 2:
                            sb.Append("제로사이다 하나");
                            break;
                        case 3:
                            sb.Append("제로환타 하나");
                            break;
                    }
                    if (j < orderData.extraDrink.Count - 1) {
                        sb.Append(", ");
                    }
                }
                sb.Append(" 추가");
            }
            if(orderData.extraSource.Count > 0) {
                if (orderData.extraSubMenu > 0 || orderData.extraDrink.Count > 0) {
                    sb.Append("하고 ");
                }
                sb.Append("소스는 ");
                for (int j = 0; j < orderData.extraSource.Count; ++j) {
                    switch (orderData.extraSource[j]) {
                        case 1:
                            sb.Append("케찹 하나");
                            break;
                        case 2:
                            sb.Append("머스타드 하나");
                            break;
                        case 3:
                            sb.Append("간장 하나");
                            break;
                    }
                    if (j < orderData.extraSource.Count - 1) {
                        sb.Append(", ");
                    }
                }
                sb.Append(" 추가");
            }
            if (orderData.extraSubMenu > 0 ||
                orderData.extraDrink.Count > 0 ||
                orderData.extraSource.Count > 0) {
                sb.Append("할게요.");
            }
            _text_Order.SetText(sb.ToString());
        }
        public void OnEvent(OrderTimeEvent e)
        {
            _image_Time.fillAmount = e.orderData.TimeRatio;
        }
        public void OnEvent(OrderCancelEvent e)
        {
            Sequence _sequence = DOTween.Sequence();
            _sequence.
                AppendCallback(() => _fail_1.SetActive(true)).
                AppendInterval(0.5f).
                AppendCallback(() => _fail_2.SetActive(true)).
                AppendInterval(0.5f).
                Append(_notice.DOAnchorPosY(-1200, 2f).SetEase(Ease.InQuad)).
                Join(_notice.DOShakeRotation(2f)).
                AppendCallback(() => _notice.gameObject.SetActive(false)).
                Play();
        }
        public void OnEvent(OrderCompleteEvent e)
        {
            Sequence _sequence = DOTween.Sequence();
            _sequence.
                Append(_image_Complete.DOFillAmount(1, 0.8f)).
                AppendInterval(0.5f).
                Append(_notice.DOAnchorPosX(280, 1f).SetEase(Ease.InQuad)).
                Join(_notice.DOShakeRotation(1f)).
                AppendCallback(() => _notice.gameObject.SetActive(false)).
                Play();
        }


        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();


    }
}
