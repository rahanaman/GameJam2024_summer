using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    /// <summary>
    /// �丮�� �ϴ� �������� ������ control
    /// ���� ���� �ִ°� ���� ���
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
