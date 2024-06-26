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
                //��� �� ���̳ʽ�
                int value = 0;
                switch (_id) {
                    case IngredientID.�ݶ�:
                        value = 50;
                        break;
                    case IngredientID.���̴�:
                        value = 50;
                        break;
                    case IngredientID.ȯŸ:
                        value = 50;
                        break;
                    case IngredientID.�ɪy:
                        value = 20;
                        break;
                    case IngredientID.�ӽ�Ÿ��:
                        value = 20;
                        break;
                    case IngredientID.����:
                        value = 20;
                        break;

                }
                Stage.Instance.Use���(value);
            }
            MainController.Instance.SetHand(IngredientID.None);
        }
    }
}
