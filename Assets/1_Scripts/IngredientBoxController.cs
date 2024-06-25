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
            //재료 돈 마이너스
            Debug.Log("aaa");
        }
        MainController.Instance.SetHand(IngredientID.None);
    }
}
