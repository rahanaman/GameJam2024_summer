using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        [SerializeField] private TextMeshProUGUI _text_Money;

        [SerializeField] private Button _button_OK;

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
            _isClick = false;
            // 여기서 text 값 미리 설정해 두기
            _text_Income.SetText($"$ {Stage.Instance.수입}");
            _text_Environment.SetText($"$ {Stage.Instance.폐기값}");
            _text_Ingredient.SetText($"$ {Stage.Instance.재료값}");
            _text_Rent.SetText($"$ {Stage.Instance.렌트값}");
            int profit = Stage.Instance.수입 - Stage.Instance.지출;
            _text_Profit.SetText((profit > 0 ? "+ " : "- ") + $"$ {Mathf.Abs(profit)}");
            _text_Profit.color = (profit > 0) ? Color.green : Color.red;
            _text_Money.SetText($"$ {GameManager.Instance.Money}");
            _bill.anchoredPosition = new Vector2(0, 1100);
            // 목적지는 y = 140;
            _image_BackgroundAlpha.color = Color.clear;
            _button_OK.gameObject.SetActive(false);
            Sequence sequence = DOTween.Sequence();
            sequence.
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

        private bool _isClick = false;
        public void OnClick()
        {
            if (_isClick) return;
            // 여기서 다음 으로 넘어가기
            Debug.Log("넘어가기");
            if(GameManager.Instance.Money > 0) {
                Sequence sequence = DOTween.Sequence();
                sequence.
                    Append(_bill.DOAnchorPosY(100, 0.25f)).
                    Append(_bill.DOAnchorPosY(1100, 1f).SetEase(Ease.OutQuart)).
                    AppendCallback(() => {
                        GameManager.Instance.Stage++;
                        SceneManager.LoadScene("Stage");
                    }).
                    Play();
            }
            else {
                Sequence sequence = DOTween.Sequence();
                sequence.
                    Append(_bill.DOShakeRotation(2f)).
                    Join(_bill.DOAnchorPosY(-850, 2f).SetEase(Ease.OutQuad)).
                    OnComplete(() => {
                        SceneManager.LoadScene("GameOver");
                    }).
                    Play();
            }
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
