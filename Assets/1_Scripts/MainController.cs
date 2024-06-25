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
            if (_instance == null) throw new System.Exception();
            return _instance;
        }
    }

    private void Awake()
    {
        
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

        _handController.SetSprite(GetSprite(id));
    }

    public Sprite GetSprite(IngredientID id)
    {
        return _ingredients[(int)id];
    }

    public void SetCookData(CookData d = null)
    {
        Data = d;
    }


}
