using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarsDonalds
{
    public class Shoot : MonoBehaviour, IDropHandler
    {
        [SerializeField]List<Image> _����;
        [SerializeField]List<Image> _����;
        [SerializeField]List<Image> _�ҽ�;
        [SerializeField] Image _�ܹ���;
        private List<CookData> _���ĵ� = new List<CookData>();
        private List<int> _����� = new List<int>();
        private List<int> _�ҽ��� = new List<int>();
        private int _�ܹ��ŵ�;


        private void Init()
        {
            _���ĵ� = new List<CookData>();
            for(int i = 0; i < 3; ++i)
            {
                _����[i].sprite = MainController.Instance.GetSprite(IngredientID.None);
            }
        } 
        public void OnDrop(PointerEventData eventData)
        {
            if (MainController.Instance.ID <= IngredientID.����) return;
            if (MainController.Instance.ID != IngredientID.None)
            {
                if(MainController.Instance.ID < IngredientID.����)
                {
                    if (_���ĵ�.Count < 3)
                    {
                        _����[_���ĵ�.Count].sprite = MainController.Instance.GetSprite(MainController.Instance.ID);
                        _���ĵ�.Add(MainController.Instance.Data);
                        MainController.Instance.SetHand(IngredientID.None);
                        MainController.Instance.SetCookData(null);
                    }
                }
                else if(MainController.Instance.ID < IngredientID.�ҽ�)
                {
                    if (_�����.Count < 3)
                    {
                        _����[_�����.Count].sprite = MainController.Instance.GetSprite(MainController.Instance.ID);
                        int d = 0;
                        switch (MainController.Instance.ID)
                        {
                            case IngredientID.�ݶ�: d = 1; break;
                            case IngredientID.���̴�: d = 2; break;
                            case IngredientID.ȯŸ: d = 3; break;
                        }
                        _�����.Add(d);
                        MainController.Instance.SetHand(IngredientID.None);
                        MainController.Instance.SetCookData(null);
                    }
                }
                else if(MainController.Instance.ID < IngredientID.�ܹ���)
                {
                    if (_�ҽ���.Count < 3)
                    {
                        _�ҽ�[_�ҽ���.Count].sprite = MainController.Instance.GetSprite(MainController.Instance.ID);
                        int d = 0;
                        switch (MainController.Instance.ID)
                        {
                            case IngredientID.�ɪy: d = 1; break;
                            case IngredientID.�ӽ�Ÿ��: d = 2; break;
                            case IngredientID.����: d = 3; break;
                        }
                        _�ҽ���.Add(d);
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
