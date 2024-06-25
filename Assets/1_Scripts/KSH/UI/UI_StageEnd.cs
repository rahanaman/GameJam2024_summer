using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MarsDonalds
{
    public class UI_StageEnd : MonoBehaviour, IEventListener<StageEndEvent>
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image_BackgroundAlpha;
        [SerializeField] private RectTransform _bill;

        [SerializeField] private TextMeshProUGUI _text_Income;
        [SerializeField] private TextMeshProUGUI _text_Environment;
        [SerializeField] private TextMeshProUGUI _text_Ingredient;
        [SerializeField] private TextMeshProUGUI _text_Rent;
        [SerializeField] private TextMeshProUGUI _text_Profit;

        [SerializeField] private Button _button_OK;

        private Sequence _sequence;

        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
            this.EventStartListening<StageEndEvent>();
        }

        public void EventStop()
        {
            this.EventStopListening<StageEndEvent>();
        }

        public void OnEvent(StageEndEvent e)
        {
            SetUIState(true);
            // 여기서 text 값 미리 설정해 두기
            _bill.anchoredPosition = new Vector2(0, 1100);
            // 목적지는 y = 140;
            _image_BackgroundAlpha.color = Color.clear;
            _button_OK.gameObject.SetActive(false);
            _sequence = DOTween.Sequence();
            _sequence.
                Append(_image_BackgroundAlpha.DOColor(Color.black, 2f)).
                Append(_bill.DOAnchorPosY(140 - 200, 0.25f).SetEase(Ease.OutQuad)).
                Append(_bill.DOAnchorPosY(140 + 100, 0.25f).SetEase(Ease.InQuad)).
                Append(_bill.DOAnchorPosY(140 - 50, 0.5f).SetEase(Ease.OutQuad)).
                Append(_bill.DOAnchorPosY(140, .5f).SetEase(Ease.InQuad)).
                AppendCallback(() => {
                    _button_OK.gameObject.SetActive(true);
                }).
                Play();
        }

        public void OnClick()
        {
            // 여기서 다음 으로 넘어가기
            Debug.Log("넘어가기");
        }

        private void SetUIState(bool state)
        {
            _canvasGroup.alpha = state ? 1 : 0;
            _canvasGroup.blocksRaycasts = state;
            _canvasGroup.interactable = state;
        }

        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
