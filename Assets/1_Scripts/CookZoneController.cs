using MarsDonalds;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public struct CutStartEvent
{
    static CutStartEvent e;
    public int value;
    public static void Trigger(int v)
    {
        e.value = v;
        EventManager.TriggerEvent(e);
    }
}

public struct CutEndEvent
{
    static CutEndEvent e;
    public static void Trigger()
    {

        EventManager.TriggerEvent(e);
    }
}


public class CookZoneController : MonoBehaviour, IDropHandler, IBeginDragHandler,IDragHandler, IEndDragHandler,IEventListener<CutStartEvent>,IEventListener<CutEndEvent>
{
    [SerializeField] Image _ingredient;
    private IngredientID _id;
    private CookData _data;
    public CookData Data {  get { return _data; } }
    private bool _isAvailable { get { return _data == null; } }
    public bool IsAvailable { get { return _isAvailable; } }
    public bool IsListening => throw new System.NotImplementedException();

    private bool _isWorking = false;
    public bool IsWorking { get { return _isWorking; } }

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
        if(!_isWorking&&_isAvailable && MainController.Instance.ID != IngredientID.None && MainController.Instance.ID < IngredientID.Waste)
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

    public void OnEvent(CutStartEvent e)
    {
        _isWorking = true;
        //상태 변경
        Data.Pill(e.value);
    }
    public void OnEvent(CutEndEvent e)
    {
        _isWorking = false;
        _id = _data.GetIngredientID();
        _ingredient.sprite = MainController.Instance.GetSprite(_id);
    }


    public void EventStart()
    {
        this.EventStartListening<CutStartEvent>();
        this.EventStartListening<CutEndEvent>();
    }

    public void EventStop()
    {
        this.EventStopListening<CutStartEvent>();
        this.EventStopListening<CutEndEvent>();
    }
    private void OnEnable() => EventStart();
    private void OnDisable() => EventStop();
}
