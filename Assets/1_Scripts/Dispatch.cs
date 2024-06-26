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
                int value = 0;
                switch (_id) {
                    case IngredientID.콜라:
                        value = 50;
                        break;
                    case IngredientID.사이다:
                        value = 50;
                        break;
                    case IngredientID.환타:
                        value = 50;
                        break;
                    case IngredientID.케챱:
                        value = 20;
                        break;
                    case IngredientID.머스타드:
                        value = 20;
                        break;
                    case IngredientID.간장:
                        value = 20;
                        break;

                }
                Stage.Instance.Use재료(value);
            }
            MainController.Instance.SetHand(IngredientID.None);
        }
    }
}
