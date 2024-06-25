using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MarsDonalds
{
    public class CookBoxInputController : MonoBehaviour, IDropHandler
    {
        private IngredientID _id;
        [SerializeField]private Image _ingredient;
        public void OnDrop(PointerEventData eventData)
        {
            if(MainController.Instance.ID != IngredientID.None)
            {
                _id = MainController.Instance.ID;
                _ingredient.sprite = MainController.Instance.GetSprite(_id);
                MainController.Instance.SetHand(IngredientID.None);
            }
        }

    }
}
