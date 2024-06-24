using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

public class MainController :MonoBehaviour
{
    private static MainController _instance;

    [SerializeField]private Sprite[] _ingredients;

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

    private void LoadData()
    {
        _ingredients = Resources.LoadAll<Sprite>("0_Images");
    }


}
