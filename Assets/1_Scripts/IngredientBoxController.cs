using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IngredientBoxController : MonoBehaviour,IBeginDragHandler,IEndDragHandler,IDragHandler
{
    [SerializeField] private IngredientID id;
    public void OnBeginDrag(PointerEventData eventData)
    {
        MainController.Instance.SetHand(id);
    }

    public void OnDrag(PointerEventData eventData)
    {
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        MainController.Instance.SetHand(IngredientID.None);
    }
}
