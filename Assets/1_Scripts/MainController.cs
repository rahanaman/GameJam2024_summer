using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class MainController :MonoBehaviour
{
    private static MainController _instance;
    [SerializeField] private HandController _handController;
    [SerializeField]private IngredientID _id;
    private Sprite[] _ingredients;
    [SerializeField] private Camera _camera;

    public IngredientID ID
    {
        get { return _id; }
    }

    public static MainController Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            LoadData();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        _handController.SetPosition(_camera.ScreenToWorldPoint(Input.mousePosition));

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


}
