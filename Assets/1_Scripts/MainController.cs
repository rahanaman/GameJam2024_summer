using MarsDonalds;
using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class MainController : MonoBehaviour
{
    private static MainController _instance;
    [SerializeField] private HandController _handController;
    [SerializeField] private IngredientID _id;
    private Sprite[] _ingredients;
    [SerializeField] private Camera _camera;

    public CookData Data { get; private set; }

    public IngredientID ID
    {
        get { return _id; }
    }

    public static MainController Instance
    {
        get {
            return _instance;
        }
    }

    public bool IsListening => throw new System.NotImplementedException();

    private void Awake()
    {
        _instance = this;
    }

    private void FixedUpdate()
    {
        _handController.SetPosition(_camera.ScreenToWorldPoint(Input.mousePosition));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            TestEvent.Trigger(Random.Range(0, 1000));
        }
    }

    private void Start()
    {
        _instance = this;
        LoadData();
    }

    private void OnDestory()
    {
        _instance = null;
    }
    private void LoadData()
    {
        _ingredients = Resources.LoadAll<Sprite>("0_Images");
    }

    public void SetHand(IngredientID id)
    {
        _id = id;
        if(Data == null) _handController.SetSprite(GetSprite(id));
        else _handController.SetSprite(GetSprite(id, Data));
    }



    public Sprite GetSprite(IngredientID id)
    {
        if (id == IngredientID.None) return FoodImage.Instance.GetSprite(0);
        switch (id)
        {
            case IngredientID.����: return FoodImage.Instance.GetSprite(87);
            case IngredientID.�ɪy: return FoodImage.Instance.GetSprite(85);
            case IngredientID.�ӽ�Ÿ��: return FoodImage.Instance.GetSprite(86);
            case IngredientID.ȯŸ: return FoodImage.Instance.GetSprite(82);
            case IngredientID.���̴�: return FoodImage.Instance.GetSprite(84);
            case IngredientID.�ݶ�: return FoodImage.Instance.GetSprite(83);
            case IngredientID.�ܹ���: return FoodImage.Instance.GetSprite(88);
        }
        if (id == IngredientID.Waste) return FoodImage.Instance.GetSprite(81);
        if (id == IngredientID.wrapwrap) return FoodImage.Instance.GetSprite(89);
        return null;
    }

    public Sprite GetSprite(IngredientID id, CookData cookData)
    {
        Debug.Log("potato"+cookData.PotatoID+"pill" + cookData.PillState + "cut" + cookData.CutState + "cook" + cookData.CookState[0]);
        if (!cookData.isFood)
        {
            return FoodImage.Instance.GetSprite(81);
        }
        if(id == IngredientID.wrapwrap)
        {
            return FoodImage.Instance.GetSprite(89);
        }
        if (id < IngredientID.����)
        {

            if (cookData.CookState.Count == 1 && cookData.CookState[0] == 0)
            {
                int i = cookData.CutState * 4 + 4 + cookData.PotatoID;
                return FoodImage.Instance.GetSprite(i);
            }
            if (cookData.CookState.Count == 2)
            {
                if (cookData.CookState[0] == 1 && cookData.CookState[1] == 2)
                {
                    int i = 72 + cookData.PotatoID;
                    return FoodImage.Instance.GetSprite(i);
                }
                if (cookData.CookState[0] == 1 && cookData.CookState[1] == 3)
                {
                    int i = 76 + cookData.PotatoID;
                    return FoodImage.Instance.GetSprite(i);
                }
                if (cookData.CookState[1] == 1)
                {
                    int i = 20 + cookData.PotatoID;
                    return FoodImage.Instance.GetSprite(i);
                }
                else
                {
                    int i = 16 + cookData.CutState * 12 + 4 * cookData.CookState[1] + cookData.PotatoID;
                    return FoodImage.Instance.GetSprite(i);
                }
            }
            else
            {
                if (cookData.CookState[0] == 1)
                {
                    int i = 20 + cookData.PotatoID;
                    return FoodImage.Instance.GetSprite(i);
                }
                else
                {
                    Debug.Log("durl");
                    int i = 16 + cookData.CutState * 12 + 4 * cookData.CookState[0] + cookData.PotatoID;
                    Debug.Log(i);
                    return FoodImage.Instance.GetSprite(i);
                }
            }

        }

        
        return null;
    }
        
    
    public void SetCookData(CookData d = null)
    {
        Data = d;
    }

   
}
