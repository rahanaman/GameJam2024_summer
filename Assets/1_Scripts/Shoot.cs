using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarsDonalds
{
    public class Shoot : MonoBehaviour, IDropHandler
    {
        [SerializeField]List<Image> _음식;
        [SerializeField]List<Image> _음료;
        [SerializeField]List<Image> _소스;
        [SerializeField] Image _햄버거;
        private List<CookData> _음식들 = new List<CookData>();
        private List<int> _음료들 = new List<int>();
        private List<int> _소스들 = new List<int>();
        private int _햄버거들;


        private void Init()
        {
            _음식들 = new List<CookData>();
            for(int i = 0; i < 3; ++i)
            {
                _음식[i].sprite = MainController.Instance.GetSprite(IngredientID.None);
            }
        } 
        public void OnDrop(PointerEventData eventData)
        {
            if (MainController.Instance.ID <= IngredientID.포장) return;
            if (MainController.Instance.ID != IngredientID.None)
            {
                if(MainController.Instance.ID < IngredientID.음료)
                {
                    if (_음식들.Count < 3)
                    {
                        _음식[_음식들.Count].sprite = MainController.Instance.GetSprite(MainController.Instance.ID);
                        _음식들.Add(MainController.Instance.Data);
                        MainController.Instance.SetHand(IngredientID.None);
                        MainController.Instance.SetCookData(null);
                    }
                }
                else if(MainController.Instance.ID < IngredientID.소스)
                {
                    if (_음료들.Count < 3)
                    {
                        _음료[_음료들.Count].sprite = MainController.Instance.GetSprite(MainController.Instance.ID);
                        int d = 0;
                        switch (MainController.Instance.ID)
                        {
                            case IngredientID.콜라: d = 1; break;
                            case IngredientID.사이다: d = 2; break;
                            case IngredientID.환타: d = 3; break;
                        }
                        _음료들.Add(d);
                        MainController.Instance.SetHand(IngredientID.None);
                        MainController.Instance.SetCookData(null);
                    }
                }
                else if(MainController.Instance.ID < IngredientID.햄버거)
                {
                    if (_소스들.Count < 3)
                    {
                        _소스[_소스들.Count].sprite = MainController.Instance.GetSprite(MainController.Instance.ID);
                        int d = 0;
                        switch (MainController.Instance.ID)
                        {
                            case IngredientID.케챱: d = 1; break;
                            case IngredientID.머스타드: d = 2; break;
                            case IngredientID.간장: d = 3; break;
                        }
                        _소스들.Add(d);
                        MainController.Instance.SetHand(IngredientID.None);
                        MainController.Instance.SetCookData(null);
                    }
                }
                else
                {

                }
                
            }
        }
    }
}
