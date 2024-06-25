using MarsDonalds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CookZoneController : MonoBehaviour, IDropHandler, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    [SerializeField] Image _ingredient;
    private IngredientID _id;
    private CookData _data;
    private bool _isAvailable { get { return _data == null; } }

    public void OnBeginDrag(PointerEventData eventData)
    {
        MainController.Instance.SetCookData(_data);
        _data = null;
        MainController.Instance.SetHand(_id);
        _id = IngredientID.None;
        _ingredient.sprite = MainController.Instance.GetSprite(IngredientID.None);
    }

    public void OnDrag(PointerEventData eventData)
    {
       
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(_isAvailable&&MainController.Instance.ID != IngredientID.None)
        {
            _data=MainController.Instance.Data;
            MainController.Instance.SetCookData();
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
            _data = MainController.Instance.Data;
            MainController.Instance.SetCookData();
        }
    }
}
