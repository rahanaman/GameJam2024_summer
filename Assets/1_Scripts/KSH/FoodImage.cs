using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MarsDonalds
{
    public class FoodImage : MonoBehaviour
    {
        public static FoodImage Instance { get; private set; } = null;

        [SerializeField]
        private Sprite[] _foodImageData;
        private Dictionary<int, Sprite> _data;

        private void Awake()
        {
            if (Instance == null) {
                Instance = this;
                Init();
                DontDestroyOnLoad(this.gameObject);
            }
            else {
                Destroy(gameObject);
            }
        }

        private void Init()
        {
            _data = new Dictionary<int, Sprite>(_foodImageData.Length);
            for(int i = 0; i < _foodImageData.Length; ++i) {
                if (_data.ContainsKey(i) == false) {
                    _data.Add(i, _foodImageData[i]);
                }
            }
        }

        public Sprite GetSprite(int index)
        {
            if(_data.TryGetValue(index, out Sprite data)) {
                return data;
            }
            return null;
        }
    }
}
