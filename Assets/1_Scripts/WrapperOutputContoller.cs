using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarsDonalds
{



    public class WrapperOutputContoller : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IEventListener<WrapEndEvent>
    {
        [SerializeField] private Image _ingredient;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private WrapperController _controller;
        [SerializeField] private IngredientID _id;
        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
            this.EventStartListening<WrapEndEvent>();
        }

        public void EventStop()
        {
            this.EventStopListening<WrapEndEvent>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            MainController.Instance.SetCookData(_controller.Data);
            _controller.SetCookData();
            MainController.Instance.SetHand(_id);
            _id = IngredientID.None;
            _ingredient.sprite = MainController.Instance.GetSprite(IngredientID.None);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (MainController.Instance.ID != IngredientID.None)
            {
                _id = MainController.Instance.ID;
                _ingredient.sprite = MainController.Instance.GetSprite(_id);
                MainController.Instance.SetHand(IngredientID.None);
                _controller.SetCookData(MainController.Instance.Data);
                MainController.Instance.SetCookData();
            }
        }

        public void OnEvent(WrapEndEvent e)
        {
            _id = IngredientID.wrapwrap;
            _ingredient.sprite = MainController.Instance.GetSprite(_id);
            _rectTransform.localPosition = new Vector3(0, 300, 0);
            _rectTransform.DOLocalMove(Vector3.zero, 0.5f);
        }
        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
