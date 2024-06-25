using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    public class Cooking : MonoBehaviour
    {
        public static Cooking Instance { get; private set; } = null;

        private void Awake()
        {
            Instance = this;
        }
    }
}
