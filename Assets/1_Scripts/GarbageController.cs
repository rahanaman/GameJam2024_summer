using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MarsDonalds
{
    public class GarbageController : MonoBehaviour,IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if(MainController.Instance.ID != IngredientID.None)
            {

            }
        }

        
    }
}
