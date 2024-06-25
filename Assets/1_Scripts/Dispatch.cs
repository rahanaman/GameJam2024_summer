using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarsDonalds
{
    public class Dispatch : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private IngredientID _id;
        public void OnBeginDrag(PointerEventData eventData)
        {
            MainController.Instance.SetHand(_id);
        }

        public void OnDrag(PointerEventData eventData)
        {

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (MainController.Instance.ID == IngredientID.None)
            {
                //재료 돈 마이너스
                Debug.Log("aaa");
            }
            MainController.Instance.SetHand(IngredientID.None);
        }
    }
}
