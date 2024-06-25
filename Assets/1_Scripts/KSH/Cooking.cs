using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    /// <summary>
    /// 요리를 하는 전반적인 과정을 control
    /// 지금 집고 있는게 뭔지 등등
    /// </summary>
    public class Cooking : MonoBehaviour
    {
        public static Cooking Instance { get; private set; } = null;


        private void Awake()
        {
            Instance = this;
        }
    }
}
