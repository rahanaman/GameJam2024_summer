using MarsDonalds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientBoxController : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private int _id;
    [SerializeField] private IngredientID _imageID;
    public void OnBeginDrag(PointerEventData eventData)
    {
        CookData data = new CookData(_id);
        MainController.Instance.SetHand(_imageID);
        MainController.Instance.SetCookData(data);
    }

    public void OnDrag(PointerEventData eventData)
    {

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(MainController.Instance.ID == IngredientID.None)
        {
            //��� �� ���̳ʽ�
            int value = 0;
            switch (_imageID) {
                case IngredientID.Potatoes:
                    value = 20;
                    break;
                case IngredientID.�����:
                    value = 30;
                    break;
                case IngredientID.������:
                    value = 30;
                    break;
                case IngredientID.��ġ��:
                    value = 30;
                    break;
            }
            Stage.Instance.Use���(value);
        }
        MainController.Instance.SetHand(IngredientID.None);
    }
}
