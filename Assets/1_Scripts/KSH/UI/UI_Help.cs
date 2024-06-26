using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MarsDonalds
{
    public class UI_Help : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;

        public void OnClick()
        {
            _gameObject.SetActive(!_gameObject.activeSelf);
        }
    }
}
