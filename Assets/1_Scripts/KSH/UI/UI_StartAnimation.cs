using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarsDonalds
{
    public struct StartAnimationEvent
    {
        static StartAnimationEvent e;
        public int value;

        public static void Trigger(int value)
        {
            e.value = value;
            EventManager.TriggerEvent(e);
        }

    }

    public struct AnimationEndEvent
    {
        static AnimationEndEvent e;
        public static void Trigger()
        {
            EventManager.TriggerEvent(e);
        }
    }


    public class UI_StartAnimation : MonoBehaviour, IEventListener<StartAnimationEvent>
    {
        [SerializeField] private Image _image_BackgroundAlpha;
        [SerializeField] private RectTransform _rect;
        [SerializeField] private TextMeshProUGUI _text;
        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
            this.EventStartListening<StartAnimationEvent>();
        }

        public void EventStop()
        {
            this.EventStopListening<StartAnimationEvent>();
        }

        public void OnEvent(StartAnimationEvent e)
        {
            _image_BackgroundAlpha.raycastTarget = true;
            _image_BackgroundAlpha.color = Color.black;
            _rect.localScale = new Vector3(5, 5, 1);
            _rect.anchoredPosition = Vector2.zero;
            _text.SetText($"Mars Day {e.value}");
            Sequence _sequence = DOTween.Sequence();
            _sequence.
                AppendInterval(2f).
                Append(_rect.DOAnchorPos(new Vector2(-795f, 440f), 1f)).
                Join(_rect.DOScale(Vector3.one, 1f)).
                Join(_image_BackgroundAlpha.DOColor(Color.clear, 1f)).
                OnComplete(() => {
                    _image_BackgroundAlpha.raycastTarget = false;
                    AnimationEndEvent.Trigger();
                }).
                Play();
        }
        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
