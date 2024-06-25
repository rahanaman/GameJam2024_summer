using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using UnityEngine.UI;

namespace MarsDonalds
{
    public class CookBoxOutputController : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler, IEventListener<CookEndEvent>
    {
        [SerializeField] private Image _ingredient;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private CookBoxController _controller;
        [SerializeField]private IngredientID _id;
        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
            this.EventStartListening<CookEndEvent>();
        }

        public void EventStop()
        {
            this.EventStopListening<CookEndEvent>();
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
            if(MainController.Instance.ID != IngredientID.None)
            {
                _id = MainController.Instance.ID;
                _ingredient.sprite = MainController.Instance.GetSprite(_id);
                MainController.Instance.SetHand(IngredientID.None);
                _controller.SetCookData(MainController.Instance.Data);
                MainController.Instance.SetCookData();
            }
        }

        public void OnEvent(CookEndEvent e)
        {
            _id = _controller.Data.GetIngredientID();
            _ingredient.sprite = MainController.Instance.GetSprite(_id);
            _rectTransform.localPosition = new Vector3(-175,0,0);
            _rectTransform.DOLocalMove(Vector3.zero, 0.5f);
        }
        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }
}
