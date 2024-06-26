using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using DG.Tweening;

namespace MarsDonalds
{

    
    public class CookBoxInputController : MonoBehaviour, IDropHandler, IEventListener<CookStartEvent>
    {

        private IngredientID _id;
        [SerializeField]private RectTransform _transform;
        [SerializeField]private Image _ingredient;
        [SerializeField]private CookBoxController _controller;
        private bool isAvailable = true;


        public bool IsListening => throw new System.NotImplementedException();

        public void EventStart()
        {
            this.EventStartListening<CookStartEvent>();
        }

        public void EventStop()
        {
            this.EventStopListening<CookStartEvent>();
        }

        public void OnDrop(PointerEventData eventData)
        {

            if(!_controller.IsCooking&&_controller.IsAvailable&&MainController.Instance.ID != IngredientID.None && MainController.Instance.ID < IngredientID.Waste && MainController.Instance.ID < IngredientID.����)
            {
                _controller.SetCookData(MainController.Instance.Data);
                MainController.Instance.SetCookData();
                _id = MainController.Instance.ID;
                _ingredient.sprite = MainController.Instance.GetSprite(_id,_controller.Data);
                MainController.Instance.SetHand(IngredientID.None);
                _transform.localPosition = Vector3.zero;
                _transform.DOLocalMove(_transform.localPosition + new Vector3(175, 0, 0), 0.5f);
                _controller.Cook();
                CookStartEvent.Trigger();
            }
        }

        public void OnEvent(CookStartEvent e)
        {
            
        }

        private void OnEnable() => EventStart();
        private void OnDisable() => EventStop();
    }

}
