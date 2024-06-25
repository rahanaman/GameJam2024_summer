using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MarsDonalds
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private RectTransform rectTransform;
        // Start is called before the first frame update
        void Start()
        {
            rectTransform.DOMoveX(rectTransform.position.x - 1, 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
