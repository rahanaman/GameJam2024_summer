using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookZoneController : MonoBehaviour, IDropHandler, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    [SerializeField] Image _ingredient;
    private IngredientID _id;

    public void OnBeginDrag(PointerEventData eventData)
    {
        MainController.Instance.SetHand(_id);
        _id = IngredientID.None;
        _ingredient.sprite = MainController.Instance.GetSprite(IngredientID.None);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Aaa");
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(MainController.Instance.ID != IngredientID.None)
        {
            _id = MainController.Instance.ID;
            _ingredient.sprite = MainController.Instance.GetSprite(MainController.Instance.ID);
            MainController.Instance.SetHand(IngredientID.None);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (MainController.Instance.ID != IngredientID.None)
        {
            _id = MainController.Instance.ID;
            _ingredient.sprite = MainController.Instance.GetSprite(MainController.Instance.ID);
            MainController.Instance.SetHand(IngredientID.None);
        }
        Debug.Log("a");
    }
}
