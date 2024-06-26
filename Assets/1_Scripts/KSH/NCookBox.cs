using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    public class NCookBox : MonoBehaviour
    {
        public int openDay;
        void Start()
        {   
            if(GameManager.Instance.Stage < openDay) {
                gameObject.SetActive(false);
            }
        
        }
    }
}
