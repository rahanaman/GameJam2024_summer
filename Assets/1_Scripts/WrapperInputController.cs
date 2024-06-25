using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarsDonalds
{
   

    public class WrapperInputController : MonoBehaviour, IDropHandler
    {

        private IngredientID _id;
        [SerializeField] private RectTransform _transform;
        [SerializeField] private Image _ingredient;
        [SerializeField] private WrapperController _controller;



        public bool IsListening => throw new System.NotImplementedException();



        public void OnDrop(PointerEventData eventData)
        {

            if (!_controller.IsCooking && _controller.IsAvailable && MainController.Instance.ID != IngredientID.None && MainController.Instance.ID < IngredientID.Waste)
            {
                _controller.SetCookData(MainController.Instance.Data);
                MainController.Instance.SetCookData();
                _id = MainController.Instance.ID;
                _ingredient.sprite = MainController.Instance.GetSprite(_id);
                MainController.Instance.SetHand(IngredientID.None);
                _transform.localPosition = Vector3.zero;
                _transform.DOLocalMove(_transform.localPosition + new Vector3(0, -300, 0), 0.5f);
                WrapStartEvent.Trigger();
            }
        }

        

    }
}
